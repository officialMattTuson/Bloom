using Bloom.Core.Models;
using Bloom.Persistence.Repositories.Interfaces;
using MongoDB.Driver;

namespace Bloom.Persistence.Repositories
{
  public class IndicatorRuleRepository : IIndicatorRuleRepository
  {
    private readonly IMongoCollection<IndicatorRule> _collection;

    public IndicatorRuleRepository(BloomDbContext context)
    {
      _collection = context.Database.GetCollection<IndicatorRule>("IndicatorRules");
    }

    public async Task<List<IndicatorRule>> GetEnabledRulesAsync(string portfolioId)
    {
      var filter = Builders<IndicatorRule>.Filter.Eq(r => r.IsEnabled, true);
      return await _collection.Find(filter).ToListAsync();
    }
  }
}