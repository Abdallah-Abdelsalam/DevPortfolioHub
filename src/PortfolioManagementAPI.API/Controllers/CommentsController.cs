using Microsoft.AspNetCore.Mvc;
using DevPortfolioHub.API.DTOs;

namespace DevPortfolioHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private static List<CommentDto> _comments = new();
    private static int _nextId = 1;

    [HttpGet("project/{projectId}")]
    public IActionResult GetCommentsByProject(int projectId)
    {
        var comments = _comments.Where(c => c.ProjectId == projectId).OrderByDescending(c => c.CreatedAt).ToList();
        return Ok(comments);
    }

    [HttpPost("project/{projectId}")]
    public IActionResult CreateComment(int projectId, [FromBody] CreateCommentDto createCommentDto)
    {
        var comment = new CommentDto
        {
            Id = _nextId++,
            Content = createCommentDto.Content,
            ProjectId = projectId,
            UserId = 1, // In production, get from authenticated user
            UserName = "currentuser",
            CreatedAt = DateTime.UtcNow
        };
        
        _comments.Add(comment);
        
        return Ok(comment);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
            return NotFound(new { message = $"Comment with ID {id} not found" });
        
        comment.Content = updateCommentDto.Content;
        
        return Ok(new { message = "Comment updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteComment(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
            return NotFound(new { message = $"Comment with ID {id} not found" });
        
        _comments.Remove(comment);
        
        return Ok(new { message = "Comment deleted successfully" });
    }
}
