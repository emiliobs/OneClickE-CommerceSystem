using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OneClick.Backend.Services;
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
    private readonly IImageService _imageService;

    // Injecting the UserManager provided by ASP.NET Core Identity and IConfiguration
    public UserController(UserManager<User> userManager, IConfiguration configuration, IImageService imageService)
    {
        this._userManager = userManager;
        this._configuration = configuration;
        this._imageService = imageService;
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

            // We initialize this variable to null, and only set it if an image is provided
            string? uploadImagenUrl = null;
            if (!string.IsNullOrEmpty(registerDTO.ImageBase64))
            {
                // If an image is provided, we upload it and get the URL to save in the database
                uploadImagenUrl = await _imageService.UploadBase64ImageAsync(registerDTO.ImageBase64);
            }

            // Map the DTO data to our real User entity
            var newUser = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.Email, // Identity requires UserName, we use Email
                Email = registerDTO.Email,
                ImageUrl = uploadImagenUrl, // Save the uploaded image URL if provided
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
            return StatusCode(500, $"An internal server error ocurred while registering the user: {ex.Message}");
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

              // We also include the Role claim so we can use it for authorization in the frontend and backend
              new Claim(ClaimTypes.Role, user.Role ?? "Customer"),

              new Claim("ImageUrl", user.ImageUrl ?? "") // Custom claim for the profile image URL
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
    public async Task<IActionResult> GetUserProfile() // Removed async Task since we don't use await here
    {
        try
        {
            // We can now directly read all the user data from the JWT claims without any string manipulation!
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email claim is missing in the token.");
            }

            // We can also fetch the user from the database if we need any additional data (like ImageUrl),
            // but for the profile display, we already have everything in the token claims.
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound("User not found.");
            }

            // We create a DTO to send only the necessary data to the frontend,
            // and we can fill it directly from the user entity or the claims
            var profileData = new UserProfileDTO
            {
                Id = user.Id,// We can include the user ID if needed for future updates
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Email = user.Email ?? "",
                Role = user.Role ?? "Customer",
                ImageUrl = user.ImageUrl // We can get the image URL directly from the database
            };

            return Ok(profileData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[UserController] Error fetching profile: {ex.Message}");
            return StatusCode(500, $"An internal server error occurred: {ex.Message}");
        }
    }

    [HttpPut("my-profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDTO userProfileDTO)
    {
        try
        {
            // We can read the email from the token claims to identify which user is making the request
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email claim is missing in the token.");
            }

            // Fetch the user from the database to update their profile
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound("User not found.");
            }

            // Update the user's profile with the new data from the DTO
            if (!string.IsNullOrEmpty(userProfileDTO.ImageBase64))
            {
                // If a new image is provided, we upload it and update the ImageUrl
                user.ImageUrl = await _imageService.UploadBase64ImageAsync(userProfileDTO.ImageBase64);
            }

            // We only allow updating the first and last name for now, but you can expand this as needed
            user.FirstName = userProfileDTO.FirstName;
            user.LastName = userProfileDTO.LastName;

            // Save the changes to the database
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // After updating the profile, we return a success message and the new image URL if it was updated
                return Ok(new { Message = "Profile update successfully!", NewImage = user.ImageUrl });
            }

            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UserController Error updating profile: {ex.Message}");
            return StatusCode(500, $"An internal server error occurred while updating the profile: {ex.Message}");
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
                Id = u.Id, // We can include the user ID if needed for future updates
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

    // PUT: api/user/change-role
    [HttpPut("change-role")]
    [Authorize(Roles = "Admin")] // The Lock: Only administrator can change user roles
    public async Task<IActionResult> ChangeUserRole([FromBody] UserRoleDTO userRoleDTO)
    {
        try
        {
            // We can read the email from the DTO to identify which user's role we want to change
            var user = await _userManager.FindByEmailAsync(userRoleDTO.Email);
            if (user is null)
            {
                return NotFound("User not fount.");
            }

            // Safety check to prevent an admin from changing their own role, which could lock them out of the system
            var currrenAdminEmail = User.FindFirstValue(ClaimTypes.Email);
            if (user.Email == currrenAdminEmail)
            {
                return BadRequest("Security protocol: You cannot change your own role.");
            }

            // Update the user's role with the new value from the DTO
            user.Role = userRoleDTO.Role;
            var result = await _userManager.UpdateAsync(user);

            // We check if the update was successful and return the appropriate response
            if (result.Succeeded)
            {
                return Ok(new { Message = $"User role updated successfully to {userRoleDTO.Role}!" });
            }

            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UserCOntroller Error changing role: {ex.Message}");
            return StatusCode(500, $"An internal server error occurred while changing user role: {ex.Message}");
        }
    }

    // DELETE: api/user/delete/{email}
    [HttpDelete("delete/{email}")] // We can identify the user to delete by their email, which is unique in our system
    [Authorize(Roles = "Admin")]// The Lock: Only administrator can delete users
    public async Task<IActionResult> DeleteUser(string email)
    {
        try
        {
            // We find the user by their email, and if they exist, we delete them from the database
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound("User not found.");
            }

            // Safety check to prevent an admin from deleting their own account, which could lock them out of the system
            var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);
            if (user.Email == currentAdminEmail)
            {
                return BadRequest("Security protocol: You cannot delete your own account.");
            }

            // We proceed to delete the user and check if the operation was successful
            var result = await _userManager.DeleteAsync(user);
            // We check if the deletion was successful and return the appropriate response
            if (result.Succeeded)
            {
                return Ok(new { Message = "User deleted successfully!" });
            }

            // If deletion fails for some reason, we return the specific errors
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"User COntroller Error deleting user: {ex.Message}");
            return StatusCode(500, $"An internal server error occurred while deleting the user: {ex.Message}");
        }
    }
}