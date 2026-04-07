using DevPortfolioHub.Core.Entities;

namespace DevPortfolioHub.Core.Interfaces;

public interface IAuthService
{
    Task<string> GenerateJwtTokenAsync(User user);
    Task<User?> AuthenticateAsync(string username, string password);
    Task<bool> RegisterAsync(string username, string email, string password);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}