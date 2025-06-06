namespace Bloom.Core.Models
{
  public class Holding
  {
    public required string Symbol { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
  }
}