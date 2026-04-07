using DevPortfolioHub.Core.Entities;

namespace DevPortfolioHub.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserWithPortfoliosAsync(int userId);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetActiveUsersAsync();
}