using Bloom.Core.Models;
using MongoDB.Driver;
using Bloom.Persistence.Repositories.Interfaces;

namespace Bloom.Persistence.Repositories
{
  public class PortfolioRepository : IPortfolioRepository
  {
    private readonly IMongoCollection<Portfolio> _portfolios;

    public PortfolioRepository(BloomDbContext context)
    {
      _portfolios = context.Database.GetCollection<Portfolio>("Portfolios");
    }

    public async Task<List<Portfolio>> GetAllAsync()
    {
      return await _portfolios.Find(_ => true).ToListAsync();
    }

    public async Task InsertAsync(Portfolio portfolio)
    {
      await _portfolios.InsertOneAsync(portfolio);
    }
  }
}
