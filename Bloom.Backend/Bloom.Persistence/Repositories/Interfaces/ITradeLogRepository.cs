using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface ITradeLogRepository
  {
    Task CreateAsync(TradeLog log);
  }
}