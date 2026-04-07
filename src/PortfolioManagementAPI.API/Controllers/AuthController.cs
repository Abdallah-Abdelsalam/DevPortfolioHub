using Microsoft.AspNetCore.Mvc;
using DevPortfolioHub.API.DTOs;

namespace DevPortfolioHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static List<UserDto> _users = new();
    private static int _nextId = 1;

    [HttpPost("register")]
    public IActionResult Register([FromBody] CreateUserDto createUserDto)
    {
        var user = new UserDto
        {
            Id = _nextId++,
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            AvatarUrl = createUserDto.AvatarUrl,
            CreatedAt = DateTime.UtcNow
        };
        
        _users.Add(user);
        
        return Ok(new { message = "Registration successful", user });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var user = _users.FirstOrDefault(u => u.Username == loginDto.Username);
        
        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });
        
        // In production, use real JWT token generation
        var token = $"sample-jwt-token-for-{user.Username}";
        
        return Ok(new AuthResponseDto
        {
            Token = token,
            User = user
        });
    }
}
