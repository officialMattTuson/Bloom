using Bloom.Core.Models;

namespace Bloom.API.Services.Interfaces
{
  public interface ITradeExecutionService
  {
    Task ExecuteTradeAsync(TradeLog log);
  }
}