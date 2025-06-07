using Bloom.Core.Models;

namespace Bloom.Persistence.Repositories.Interfaces
{
  public interface IIndicatorRuleRepository
  {
    Task<List<IndicatorRule>> GetEnabledRulesAsync(string portfolioId);
  }
}