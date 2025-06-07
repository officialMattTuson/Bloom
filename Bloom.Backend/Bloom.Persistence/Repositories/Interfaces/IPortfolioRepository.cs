using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface IPortfolioRepository
  {
    Task<List<Portfolio>> GetAllAsync();
    Task<Portfolio?> GetByIdAsync(string id);
    Task CreateAsync(Portfolio portfolio);
    Task<bool> UpdateAsync(string id, Portfolio portfolio);
    Task<bool> DeleteAsync(string id);

  }
}
