using Bloom.Core.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;

namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SeederController : ControllerBase
  {
    private readonly IMongoCollection<IndicatorRule> _rules;
    private readonly IMongoCollection<TickerIndicatorData> _indicators;
    private readonly IMongoCollection<PortfolioTransaction> _transactions;

    public SeederController(Bloom.Persistence.BloomDbContext context)
    {
      _rules = context.Database.GetCollection<IndicatorRule>("IndicatorRules");
      _indicators = context.Database.GetCollection<TickerIndicatorData>("TickerIndicators");
      _transactions = context.Database.GetCollection<PortfolioTransaction>("PortfolioTransactions");
    }

    [HttpPost("rule")]
    public async Task<IActionResult> InsertRule([FromBody] IndicatorRule rule)
    {
      rule.CreatedAt = DateTime.UtcNow;
      await _rules.InsertOneAsync(rule);
      return Ok(rule);
    }

    [HttpPost("indicator")]
    public async Task<IActionResult> InsertIndicator([FromBody] TickerIndicatorData data)
    {
      data.LastUpdated = DateTime.UtcNow;
      await _indicators.InsertOneAsync(data);
      return Ok(data);
    }

    [HttpGet("transactions/{portfolioId}")]
    public async Task<IActionResult> GetTransactions(string portfolioId)
    {
      var filter = Builders<PortfolioTransaction>.Filter.Eq(t => t.PortfolioId, portfolioId);
      var results = await _transactions.Find(filter).ToListAsync();
      return Ok(results);
    }
  }
}
