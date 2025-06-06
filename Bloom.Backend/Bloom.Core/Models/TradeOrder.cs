namespace Bloom.Core.Models
{
  public class TradeOrder
  {
    public required string Symbol { get; set; }
    public int Quantity { get; set; }
    public required string Side { get; set; }
    public DateTime ExecutedAt { get; set; }
  }
}