using Bloom.Core.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;

namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TransactionsController : ControllerBase
  {
    private readonly IMongoCollection<PortfolioTransaction> _transactions;
    private readonly IMongoCollection<Portfolio> _portfolios;

    public TransactionsController(Bloom.Persistence.BloomDbContext context)
    {
      _transactions = context.Database.GetCollection<PortfolioTransaction>("PortfolioTransactions");
      _portfolios = context.Database.GetCollection<Portfolio>("Portfolios");
    }

    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetByPortfolio(string portfolioId, [FromQuery] string? symbol = null)
    {
      var filter = Builders<PortfolioTransaction>.Filter.Eq(t => t.PortfolioId, portfolioId);
      if (!string.IsNullOrWhiteSpace(symbol))
      {
        filter &= Builders<PortfolioTransaction>.Filter.Eq(t => t.Symbol, symbol);
      }

      var results = await _transactions.Find(filter)
          .SortByDescending(t => t.ExecutedAt)
          .ToListAsync();

      return Ok(results);
    }

    [HttpGet("{portfolioId}/summary")]
    public async Task<IActionResult> GetSummary(string portfolioId)
    {
      var portfolio = await _portfolios.Find(p => p.Id == portfolioId).FirstOrDefaultAsync();
      if (portfolio == null) return NotFound();

      var summary = new
      {
        portfolio.Summary.TotalValue,
        portfolio.Summary.TotalReturn,
        portfolio.Summary.TotalReturnPercent,
        portfolio.Summary.AverageReturn,
        portfolio.Summary.Positions,
        portfolio.Summary.RealizedGainTotal,
        portfolio.Summary.RealizedGainPercent,
        portfolio.InitialCapital,
        portfolio.CashBalance
      };

      return Ok(summary);
    }
  }
}