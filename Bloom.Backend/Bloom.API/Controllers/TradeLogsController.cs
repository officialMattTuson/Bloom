using Bloom.Core.Enums;
using Bloom.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TradeLogsController : ControllerBase
  {
    private readonly IMongoCollection<TradeLog> _tradeLogs;

    public TradeLogsController(Bloom.Persistence.BloomDbContext context)
    {
      _tradeLogs = context.Database.GetCollection<TradeLog>("TradeLogs");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var results = await _tradeLogs.Find(_ => true)
          .SortByDescending(t => t.ExecutedAt)
          .ToListAsync();
      return Ok(results);
    }

    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetByPortfolio(string portfolioId)
    {
      var results = await _tradeLogs.Find(t => t.PortfolioId == portfolioId)
          .SortByDescending(t => t.ExecutedAt)
          .ToListAsync();
      return Ok(results);
    }

    [HttpGet("{portfolioId}/summary")]
    public async Task<IActionResult> GetSummary(string portfolioId)
    {
      var logs = await _tradeLogs.Find(t => t.PortfolioId == portfolioId).ToListAsync();
      var totalLogs = logs.Count;
      var totalBuys = logs.Count(l => l.Action == TradeAction.Buy);
      var totalSells = logs.Count(l => l.Action == TradeAction.Sell);

      return Ok(new
      {
        TotalLogs = totalLogs,
        TotalBuys = totalBuys,
        TotalSells = totalSells
      });
    }

    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] TradeLogQueryRequest request)
    {
      var builder = Builders<TradeLog>.Filter;
      var filter = builder.Empty;

      if (!string.IsNullOrEmpty(request.PortfolioId))
        filter &= builder.Eq(t => t.PortfolioId, request.PortfolioId);

      if (!string.IsNullOrEmpty(request.Symbol))
        filter &= builder.Eq(t => t.Symbol, request.Symbol);

      if (request.Action.HasValue)
        filter &= builder.Eq(t => t.Action, request.Action.Value);

      if (request.From.HasValue)
        filter &= builder.Gte(t => t.ExecutedAt, request.From.Value);

      if (request.To.HasValue)
        filter &= builder.Lte(t => t.ExecutedAt, request.To.Value);

      var totalCount = await _tradeLogs.CountDocumentsAsync(filter);

      var sortBuilder = Builders<TradeLog>.Sort;
      SortDefinition<TradeLog> sort = request.SortOrder == 1
          ? sortBuilder.Ascending(request.SortField)
          : sortBuilder.Descending(request.SortField);

      var results = await _tradeLogs.Find(filter)
          .Sort(sort)
          .Skip(request.Skip)
          .Limit(request.Limit)
          .ToListAsync();

      return Ok(new
      {
        Total = totalCount,
        Results = results,
        Skip = request.Skip,
        Limit = request.Limit,
        SortField = request.SortField,
        SortOrder = request.SortOrder
      });
    }
  }
}