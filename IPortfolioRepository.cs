using DevPortfolioHub.Core.Entities;

namespace DevPortfolioHub.Core.Interfaces;

public interface IPortfolioRepository : IRepository<Portfolio>
{
    Task<IEnumerable<Portfolio>> GetPortfoliosByUserIdAsync(int userId);
    Task<Portfolio?> GetPortfolioWithProjectsAsync(int portfolioId);
}