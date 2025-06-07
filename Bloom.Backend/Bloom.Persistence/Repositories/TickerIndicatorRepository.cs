using Bloom.Core.Models;
using Bloom.Persistence.Repositories.Interfaces;
using MongoDB.Driver;

namespace Bloom.Persistence.Repositories
{
  public class TickerIndicatorRepository : ITickerIndicatorRepository
  {
    private readonly IMongoCollection<TickerIndicatorData> _collection;

    public TickerIndicatorRepository(BloomDbContext context)
    {
      _collection = context.Database.GetCollection<TickerIndicatorData>("TickerIndicators");
    }

    public async Task<Dictionary<string, TickerIndicatorData>> GetAllAsync()
    {
      var all = await _collection.Find(_ => true).ToListAsync();
      return all.ToDictionary(i => i.Symbol);
    }
  }
}