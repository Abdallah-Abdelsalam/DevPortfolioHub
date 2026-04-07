using DevPortfolioHub.Core.Entities;

namespace DevPortfolioHub.Core.Interfaces;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsByProjectIdAsync(int projectId);
    Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int userId);
}