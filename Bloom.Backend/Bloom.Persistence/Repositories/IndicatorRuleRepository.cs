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

    public async Task<List<IndicatorRule>> GetAllAsync(string? symbol = null)
    {
      var filter = string.IsNullOrEmpty(symbol)
          ? Builders<IndicatorRule>.Filter.Empty
          : Builders<IndicatorRule>.Filter.Eq(r => r.Symbol, symbol);

      return await _collection.Find(filter).ToListAsync();
    }

    public async Task<IndicatorRule?> GetByIdAsync(string id)
    {
      return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(IndicatorRule rule)
    {
      await _collection.InsertOneAsync(rule);
    }

    public async Task<bool> UpdateAsync(string id, IndicatorRule rule)
    {
      var result = await _collection.ReplaceOneAsync(r => r.Id == id, rule);
      return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
      var result = await _collection.DeleteOneAsync(r => r.Id == id);
      return result.DeletedCount > 0;
    }
  }
}