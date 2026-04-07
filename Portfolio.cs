namespace DevPortfolioHub.Core.Entities;

/// <summary>
/// Portfolio entity representing a user's portfolio collection
/// </summary>
public class Portfolio : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}