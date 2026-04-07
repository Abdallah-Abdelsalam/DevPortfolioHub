using DevPortfolioHub.Core.Entities;

namespace DevPortfolioHub.Core.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetProjectsByPortfolioIdAsync(int portfolioId);
    Task<Project?> GetProjectWithCommentsAsync(int projectId);
    Task<IEnumerable<Project>> GetPopularProjectsAsync(int count);
    Task<bool> ToggleLikeAsync(int projectId, int userId);
    Task<int> GetLikeCountAsync(int projectId);
}