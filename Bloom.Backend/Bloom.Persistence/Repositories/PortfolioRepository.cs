using Bloom.Core.Models;
using Bloom.Persistence.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

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

    public async Task<Portfolio?> GetByIdAsync(string id)
    {
      if (!ObjectId.TryParse(id, out var objectId)) return null;

      return await _portfolios.Find(p => p.Id == objectId.ToString()).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Portfolio portfolio)
    {
      await _portfolios.InsertOneAsync(portfolio);
    }

    public async Task<bool> UpdateAsync(string id, Portfolio portfolio)
    {
      if (!ObjectId.TryParse(id, out var objectId)) return false;

      var result = await _portfolios.ReplaceOneAsync(p => p.Id == objectId.ToString(), portfolio);
      return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
      if (!ObjectId.TryParse(id, out var objectId)) return false;

      var result = await _portfolios.DeleteOneAsync(p => p.Id == objectId.ToString());
      return result.DeletedCount > 0;
    }
  }
}
