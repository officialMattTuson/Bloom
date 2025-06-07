using Bloom.API.Services.Interfaces;
using Bloom.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PortfolioController : ControllerBase
  {
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
      _portfolioService = portfolioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Portfolio>>> GetAll()
    {
      var portfolios = await _portfolioService.GetAllAsync();
      return Ok(portfolios);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest request)
    {
      var positions = request.Positions;

      var summary = new PortfolioSummary
      {
        TotalValue = positions.Sum(p => p.Value),
        TotalReturn = positions.Sum(p => p.UnrealizedGain),
        TotalReturnPercent = Math.Round(positions.Average(p => p.ReturnPercent), 2),
        DailyChange = 0,
        DailyChangePercent = 0,
        Positions = positions.Count,
        AverageReturn = Math.Round(positions.Average(p => p.ReturnPercent), 2)
      };

      var portfolio = new Portfolio
      {
        Summary = summary,
        Positions = positions
      };

      await _portfolioService.CreateAsync(portfolio);
      return Ok();
    }

  }
}
