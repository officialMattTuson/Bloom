using Bloom.Core.Models;
using Bloom.Persistence.Repositories.Interfaces;
using MongoDB.Driver;

namespace Bloom.Persistence.Repositories
{

  public class PortfolioTransactionRepository : IPortfolioTransactionRepository
  {
    private readonly IMongoCollection<PortfolioTransaction> _collection;

    public PortfolioTransactionRepository(BloomDbContext context)
    {
      _collection = context.Database.GetCollection<PortfolioTransaction>("PortfolioTransactions");
    }

    public async Task CreateAsync(PortfolioTransaction tx)
    {
      await _collection.InsertOneAsync(tx);
    }
  }
}