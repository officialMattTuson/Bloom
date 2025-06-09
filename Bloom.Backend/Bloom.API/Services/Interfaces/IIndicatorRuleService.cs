using Bloom.Core.Models;

namespace Bloom.API.Services.Interfaces;

public interface IIndicatorRuleService
{
  Task<List<IndicatorRule>> GetAllAsync(string? symbol = null);
  Task<IndicatorRule?> GetByIdAsync(string id);
  Task CreateAsync(IndicatorRule rule);
  Task<bool> UpdateAsync(string id, IndicatorRule rule);
  Task<bool> DeleteAsync(string id);
}
