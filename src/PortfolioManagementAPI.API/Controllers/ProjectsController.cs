using Microsoft.AspNetCore.Mvc;
using DevPortfolioHub.API.DTOs;

namespace DevPortfolioHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private static List<ProjectDto> _projects = new();
    private static List<CommentDto> _comments = new();
    private static int _nextProjectId = 1;

    [HttpGet]
    public IActionResult GetProjects([FromQuery] PaginationParams paginationParams)
    {
        var query = _projects.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(paginationParams.SearchTerm))
        {
            query = query.Where(p => p.Name.Contains(paginationParams.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                                      p.Description.Contains(paginationParams.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        
        var totalCount = query.Count();
        var items = query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                         .Take(paginationParams.PageSize)
                         .ToList();
        
        var result = new PagedResult<ProjectDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetProject(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound(new { message = $"Project with ID {id} not found" });
        
        var projectComments = _comments.Where(c => c.ProjectId == id).ToList();
        var projectDetail = new ProjectDetailDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            ProjectUrl = project.ProjectUrl,
            RepositoryUrl = project.RepositoryUrl,
            ImageUrl = project.ImageUrl,
            PortfolioId = project.PortfolioId,
            CreatedAt = project.CreatedAt,
            LikeCount = project.LikeCount,
            CommentCount = project.CommentCount,
            Comments = projectComments
        };
        
        return Ok(projectDetail);
    }

    [HttpPost("portfolio/{portfolioId}")]
    public IActionResult CreateProject(int portfolioId, [FromBody] CreateProjectDto createProjectDto)
    {
        var project = new ProjectDto
        {
            Id = _nextProjectId++,
            Name = createProjectDto.Name,
            Description = createProjectDto.Description,
            ProjectUrl = createProjectDto.ProjectUrl,
            RepositoryUrl = createProjectDto.RepositoryUrl,
            ImageUrl = createProjectDto.ImageUrl,
            PortfolioId = portfolioId,
            CreatedAt = DateTime.UtcNow,
            LikeCount = 0,
            CommentCount = 0
        };
        
        _projects.Add(project);
        
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto updateProjectDto)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound(new { message = $"Project with ID {id} not found" });
        
        project.Name = updateProjectDto.Name;
        project.Description = updateProjectDto.Description;
        project.ProjectUrl = updateProjectDto.ProjectUrl;
        project.RepositoryUrl = updateProjectDto.RepositoryUrl;
        project.ImageUrl = updateProjectDto.ImageUrl;
        
        return Ok(new { message = "Project updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound(new { message = $"Project with ID {id} not found" });
        
        _projects.Remove(project);
        
        return Ok(new { message = "Project deleted successfully" });
    }

    [HttpPost("{id}/like")]
    public IActionResult ToggleLike(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound(new { message = $"Project with ID {id} not found" });
        
        // Toggle like (simplified - in real app, track per user)
        project.LikeCount++;
        
        return Ok(new { message = "Project liked", liked = true });
    }
}
