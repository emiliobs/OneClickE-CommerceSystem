using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var user = await _userManager.FindByNameAsync(loginDTO.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return Unauthorized(new { Message = "Sorry! Invalid email or password." });
            }

            // ----- THE FIX: Pack ALL the data correctly into the Token! -----
            var authClaims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Email, user.Email!),

              // We keep this one so the Blazor Top Menu says "Hi, [FirstName]!"
              new Claim(ClaimTypes.Name, user.FirstName ?? ""),

              // We use the official JWT tags for First and Last name
              new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
              new Claim(ClaimTypes.Surname, user.LastName ?? ""),

              new Claim(ClaimTypes.Role, user.Role ?? "Customer")
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Issuer"],
                  audience: _configuration["Jwt:Audience"],
                  expires: DateTime.Now.AddHours(3),
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
              );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Message = "Login successfully!"
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserController] Error during login: {ex.Message}");
            return StatusCode(500, "An internal server error occurred while logging in.");
        }
    }

    // GET: api/user/my-profile
    [HttpGet("my-profile")]
    [Authorize]
    public IActionResult GetUserProfile() // Removed async Task since we don't use await here
    {
        try
        {
            // ----- THE FIX: Read the exact tags we packed during Login -----
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            var firstName = User.FindFirstValue(ClaimTypes.GivenName) ?? "";
            var lastName = User.FindFirstValue(ClaimTypes.Surname) ?? "";
            var role = User.FindFirstValue(ClaimTypes.Role) ?? "Customer";

            // No more string splitting! We just map the DTO perfectly.
            var profileData = new UserProfileDTO
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Role = role,
            };

            return Ok(profileData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserController] Error fetching profile: {ex.Message}");
            return StatusCode(500, $"An internal server error occurred: {ex.Message}");
        }
    }

    // GET: api/user/all
    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "Admin")]// The LOck: Only administrator can use the user list
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            // Fetch all users directly from the Identity UserManager
            var users = _userManager.Users.ToList();

            // Map the database users to our safe DTO (we never send passwords to the frontend!)
            var userList = users.Select(u => new UserProfileDTO
            {
                FirstName = u.FirstName ?? "",
                LastName = u.LastName ?? "",
                Email = u.Email ?? "",
                Role = u.Role ?? "Customer"
            }).ToList();

            return Ok(userList);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserController] Error fetching all users: {ex.Message}");
            return StatusCode(500, $"An internal server error occurred while fetching users: {ex.Message}");
        }
    }
}