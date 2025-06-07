using Bloom.Core.Models;
using MongoDB.Driver;
using Bloom.Persistence.Repositories.Interfaces;

namespace Bloom.Persistence.Repositories
{
  public class PositionRepository : IPositionRepository
  {
    private readonly IMongoCollection<Position> _positions;

    public PositionRepository(BloomDbContext context)
    {
      _positions = context.Database.GetCollection<Position>("Positions");
    }

    public async Task<List<Position>> GetAllAsync()
    {
      return await _positions.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Position position)
    {
      await _positions.InsertOneAsync(position);
    }
  }
}
