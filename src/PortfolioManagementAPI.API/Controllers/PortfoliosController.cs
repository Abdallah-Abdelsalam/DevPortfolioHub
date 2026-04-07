using Microsoft.AspNetCore.Mvc;
using DevPortfolioHub.API.DTOs;

namespace DevPortfolioHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfoliosController : ControllerBase
{
    private static List<PortfolioDto> _portfolios = new();
    private static int _nextId = 1;

    [HttpGet]
    public IActionResult GetPortfolios([FromQuery] PaginationParams paginationParams)
    {
        var query = _portfolios.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(paginationParams.SearchTerm))
        {
            query = query.Where(p => p.Title.Contains(paginationParams.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        
        var totalCount = query.Count();
        var items = query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                         .Take(paginationParams.PageSize)
                         .ToList();
        
        var result = new PagedResult<PortfolioDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetPortfolio(int id)
    {
        var portfolio = _portfolios.FirstOrDefault(p => p.Id == id);
        if (portfolio == null)
            return NotFound(new { message = $"Portfolio with ID {id} not found" });
        
        return Ok(portfolio);
    }

    [HttpPost]
    public IActionResult CreatePortfolio([FromBody] CreatePortfolioDto createPortfolioDto)
    {
        var portfolio = new PortfolioDto
        {
            Id = _nextId++,
            Title = createPortfolioDto.Title,
            Description = createPortfolioDto.Description,
            CoverImageUrl = createPortfolioDto.CoverImageUrl,
            UserId = 1, // In production, get from authenticated user
            UserName = "currentuser",
            CreatedAt = DateTime.UtcNow,
            ProjectCount = 0
        };
        
        _portfolios.Add(portfolio);
        
        return CreatedAtAction(nameof(GetPortfolio), new { id = portfolio.Id }, portfolio);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePortfolio(int id, [FromBody] UpdatePortfolioDto updatePortfolioDto)
    {
        var portfolio = _portfolios.FirstOrDefault(p => p.Id == id);
        if (portfolio == null)
            return NotFound(new { message = $"Portfolio with ID {id} not found" });
        
        portfolio.Title = updatePortfolioDto.Title;
        portfolio.Description = updatePortfolioDto.Description;
        portfolio.CoverImageUrl = updatePortfolioDto.CoverImageUrl;
        
        return Ok(new { message = "Portfolio updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePortfolio(int id)
    {
        var portfolio = _portfolios.FirstOrDefault(p => p.Id == id);
        if (portfolio == null)
            return NotFound(new { message = $"Portfolio with ID {id} not found" });
        
        _portfolios.Remove(portfolio);
        
        return Ok(new { message = "Portfolio deleted successfully" });
    }
}
