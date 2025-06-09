using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface IIndicatorRuleRepository
  {
    Task<List<IndicatorRule>> GetEnabledRulesAsync(string portfolioId);
    Task<List<IndicatorRule>> GetAllAsync(string? symbol = null);
    Task<IndicatorRule?> GetByIdAsync(string id);
    Task CreateAsync(IndicatorRule rule);
    Task<bool> UpdateAsync(string id, IndicatorRule rule);
    Task<bool> DeleteAsync(string id);
  }
}