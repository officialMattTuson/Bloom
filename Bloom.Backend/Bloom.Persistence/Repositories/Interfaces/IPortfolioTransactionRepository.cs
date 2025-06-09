using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface IPortfolioTransactionRepository
  {
    Task CreateAsync(PortfolioTransaction tx);
  }
}