using Bloom.Core.Models;
using Bloom.API.Services.Interfaces;
using Bloom.Persistence.Repositories.Interfaces;

namespace Bloom.API.Services
{

  public class IndicatorRuleService : IIndicatorRuleService
  {
    private readonly IIndicatorRuleRepository _repository;

    public IndicatorRuleService(IIndicatorRuleRepository repository)
    {
      _repository = repository;
    }

    public Task<List<IndicatorRule>> GetAllAsync(string? symbol = null) =>
        _repository.GetAllAsync(symbol);

    public Task<IndicatorRule?> GetByIdAsync(string id) =>
        _repository.GetByIdAsync(id);

    public Task CreateAsync(IndicatorRule rule) =>
        _repository.CreateAsync(rule);

    public Task<bool> UpdateAsync(string id, IndicatorRule rule) =>
        _repository.UpdateAsync(id, rule);

    public Task<bool> DeleteAsync(string id) =>
        _repository.DeleteAsync(id);
  }
}