namespace DevPortfolioHub.API.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? ImageUrl { get; set; }
    public int PortfolioId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
}

public class CreateProjectDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? ImageUrl { get; set; }
}

public class UpdateProjectDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? ImageUrl { get; set; }
}

public class ProjectDetailDto : ProjectDto
{
    public List<CommentDto> Comments { get; set; } = new();
}
