using Bloom.Core.Models;

namespace Bloom.API.Services.Interfaces
{
  public interface ITradingService
  {
    Task<List<RuleEvaluationResult>> EvaluateAndExecuteAsync(string portfolioId);
  }
}