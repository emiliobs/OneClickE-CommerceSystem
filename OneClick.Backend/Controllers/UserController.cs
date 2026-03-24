using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OneClick.Shared.DTOs;
using OneClick.Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OneClick.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration; // Added to read appsettings.json

    // Injecting the UserManager provided by ASP.NET Core Identity and IConfiguration
    public UserController(UserManager<User> userManager, IConfiguration configuration)
    {
        this._userManager = userManager;
        this._configuration = configuration;
    }

    // POST: api/user/register
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO registerDTO)
    {
        try
        {
            // Check if the provided data is valid based on the DTO rules
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if a user with this email already exist in the database
            var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("A user with this email already exists.");
            }

            // Map the DTO data to our real User entity
            var newUser = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.Email, // Identity requires UserName, we use Email
                Email = registerDTO.Email,
                Role = "Customer" // Default role for new registration
            };

            // Create the user and hash the passsword automatically
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully!" });
            }

            // If creation fails (e.g., password is too weak), return the specific errors
            var errors = result.Errors.Select(e => e.Description);

            return BadRequest(new { Errors = errors });
        }
        catch (Exception ex)
        {
            // Log the error securely and return a generic 500 error to avoid exposing  server details
            Console.WriteLine($"User Controlle. Error during registration: {ex.Message}");
            return StatusCode(500, "An internal server error ocurred while registering the user.");
        }
    }

    // POST: api/user/login
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginDTO)
    {
        try
        {
            // Find the user by Email
            var user = await _userManager.FindByNameAsync(loginDTO.Email);

            // If user doesn't exist OR the password is wrong, reject them
            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return Unauthorized(new { Message = "Sorry!. Invalid email or password." });
            }

            // Create the "Claims" (the pieces of information hidden inside the token)
            var authClaims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Email, user.Email!),
              new Claim(ClaimTypes.Name, user.FirstName),
              new Claim(ClaimTypes.Role, user.Role), // I include the Role for blazor to check later!
            };

            // Get the secret key from appsettings.json
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Build the actual JWT Token
            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Issuer"],
                  audience: _configuration["Jwt:Audience"],
                  expires: DateTime.Now.AddHours(3), // Token is valid for 3 hours
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
              );

            // Return the Token to the user (Like them a hotel keycard)
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Message = "Login successfully!"
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User Controller. Error during login: {ex.Message}");
            return StatusCode(500, "An internal server arror ocurred while logging in.");
        }
    }
}