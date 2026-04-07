using Microsoft.AspNetCore.Mvc;

namespace DevPortfolioHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { 
            message = "Portfolio Management API is running!",
            status = "healthy",
            timestamp = DateTime.Now
        });
    }
}
