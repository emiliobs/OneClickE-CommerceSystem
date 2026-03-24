using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneClick.Shared.DTOs;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    // Injecting the UserManager provided by ASP.NET Core Identity
    public UserController(UserManager<User> userManager)
    {
        this._userManager = userManager;
    }

    // POST: api/user/register
    [HttpPost]
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
}