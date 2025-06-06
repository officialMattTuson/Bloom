namespace Bloom.Core.Models
{
  public class TradeSignal
  {
    public required string Symbol { get; set; }
    public required string Action { get; set; }
    public required float Confidence { get; set; }
  }
}