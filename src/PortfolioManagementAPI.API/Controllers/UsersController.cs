using Microsoft.AspNetCore.Mvc;
using DevPortfolioHub.API.DTOs;

namespace DevPortfolioHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<UserDto> _users = new()
    {
        new UserDto { Id = 1, Username = "john_doe", Email = "john@example.com", FirstName = "John", LastName = "Doe", CreatedAt = DateTime.UtcNow },
        new UserDto { Id = 2, Username = "jane_smith", Email = "jane@example.com", FirstName = "Jane", LastName = "Smith", CreatedAt = DateTime.UtcNow }
    };

    [HttpGet]
    public IActionResult GetUsers([FromQuery] PaginationParams paginationParams)
    {
        var query = _users.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(paginationParams.SearchTerm))
        {
            query = query.Where(u => u.Username.Contains(paginationParams.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                      u.Email.Contains(paginationParams.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        
        var totalCount = query.Count();
        var items = query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                         .Take(paginationParams.PageSize)
                         .ToList();
        
        var result = new PagedResult<UserDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound(new { message = $"User with ID {id} not found" });
        
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound(new { message = $"User with ID {id} not found" });
        
        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.AvatarUrl = updateUserDto.AvatarUrl;
        
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound(new { message = $"User with ID {id} not found" });
        
        _users.Remove(user);
        
        return Ok(new { message = "User deleted successfully" });
    }
}
