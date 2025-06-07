namespace Bloom.Core.Models
{
  public class RuleEvaluationResult
  {
    public required string Symbol { get; set; }
    public required IndicatorRule Rule { get; set; }
    public bool Passed { get; set; }
    public string? Reason { get; set; }
  }
}