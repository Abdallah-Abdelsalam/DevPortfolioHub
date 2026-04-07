using System.Text.Json.Serialization;

namespace DevPortfolioHub.Core.Entities;

/// <summary>
/// User entity representing an application user
/// </summary>
public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }

    // Navigation properties
    public virtual ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    public virtual ICollection<ProjectLike> LikedProjects { get; set; } = new List<ProjectLike>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}