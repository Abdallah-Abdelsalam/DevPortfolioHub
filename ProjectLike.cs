namespace DevPortfolioHub.Core.Entities;

/// <summary>
/// Join entity for many-to-many relationship between Users and Projects (Likes)
/// </summary>
public class ProjectLike
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public DateTime LikedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;
}