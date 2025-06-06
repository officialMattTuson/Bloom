namespace Bloom.Core.Models
{
  public class MarketTick
  {
    public required string Symbol { get; set; }
    public decimal Price { get; set; }
    public decimal Change { get; set; }
    public DateTime Time { get; set; }
  }
}