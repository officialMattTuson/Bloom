using Bloom.Core.Models;

namespace Bloom.API.Services.Interfaces
{
  public interface IPortfolioService
  {
    Task<List<Portfolio>> GetAllAsync();
    Task CreateAsync(Portfolio portfolio);
  }
}
