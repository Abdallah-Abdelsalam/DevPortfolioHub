namespace DevPortfolioHub.Core.Entities;

/// <summary>
/// Comment entity for project feedback
/// </summary>
public class Comment : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public virtual Project Project { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}