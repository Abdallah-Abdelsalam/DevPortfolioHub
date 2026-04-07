namespace DevPortfolioHub.Core.Entities;

/// <summary>
/// Project entity representing a project within a portfolio
/// </summary>
public class Project : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? ImageUrl { get; set; }
    public int PortfolioId { get; set; }

    // Navigation properties
    public virtual Portfolio Portfolio { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<ProjectLike> Likes { get; set; } = new List<ProjectLike>();
}