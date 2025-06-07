using Bloom.Core.Models;
using Bloom.Persistence.Repositories.Interfaces;
using MongoDB.Driver;

namespace Bloom.Persistence.Repositories
{
  public class TradeLogRepository : ITradeLogRepository
  {
    private readonly IMongoCollection<TradeLog> _collection;

    public TradeLogRepository(BloomDbContext context)
    {
      _collection = context.Database.GetCollection<TradeLog>("TradeLogs");
    }

    public async Task CreateAsync(TradeLog log)
    {
      await _collection.InsertOneAsync(log);
    }
  }
}