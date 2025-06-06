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
    private readonly ITradingService _tradingService;

    public PortfolioController(IPortfolioService portfolioService, ITradingService tradingService)
    {
      _portfolioService = portfolioService;
      _tradingService = tradingService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Portfolio>>> GetAll()
    {
      var portfolios = await _portfolioService.GetAllAsync();
      return Ok(portfolios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Portfolio>> GetById(string id)
    {
      var portfolio = await _portfolioService.GetByIdAsync(id);
      if (portfolio == null)
        return NotFound();
      return Ok(portfolio);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Portfolio updated)
    {
      var success = await _portfolioService.UpdateAsync(id, updated);
      if (!success)
        return NotFound();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var success = await _portfolioService.DeleteAsync(id);
      if (!success)
        return NotFound();
      return NoContent();
    }

    [HttpPost("{id}/evaluate")]
    public async Task<IActionResult> EvaluateAndExecuteRules(string id)
    {
      var portfolio = await _portfolioService.GetByIdAsync(id);
      if (portfolio == null) return NotFound("Portfolio not found");

      var results = await _tradingService.EvaluateAndExecuteAsync(id);
      return Ok(results);
    }

    [HttpGet("{id}/positions")]
    public async Task<IActionResult> GetPositions(string id)
    {
      var portfolio = await _portfolioService.GetByIdAsync(id);
      if (portfolio == null) return NotFound("Portfolio not found");

      return Ok(portfolio.Positions ?? new List<Position>());
    }

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> GetSummary(string id)
    {
      var portfolio = await _portfolioService.GetByIdAsync(id);
      if (portfolio == null) return NotFound("Portfolio not found");

      return Ok(portfolio.Summary);
    }

  }
}
