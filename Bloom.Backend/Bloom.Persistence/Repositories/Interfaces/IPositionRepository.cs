using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface IPositionRepository
  {
    Task<List<Position>> GetAllAsync();
    Task AddAsync(Position position);
  }
}
