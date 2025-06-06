using Bloom.Core.Models;

namespace Bloom.API.Services.Interfaces
{
  public interface IPortfolioService
  {
    Task<List<Portfolio>> GetAllAsync();
    Task CreateAsync(Portfolio portfolio);
    Task<Portfolio?> GetByIdAsync(string id);
    Task<bool> UpdateAsync(string id, Portfolio updated);
    Task<bool> DeleteAsync(string id);

  }
}
