using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface ITickerIndicatorRepository
  {
    Task<Dictionary<string, TickerIndicatorData>> GetAllAsync();
  }
}