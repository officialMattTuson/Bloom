using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface IPortfolioRepository
  {
    Task<List<Portfolio>> GetAllAsync();
    Task InsertAsync(Portfolio portfolio);
  }
}
