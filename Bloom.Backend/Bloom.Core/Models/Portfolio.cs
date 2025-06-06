namespace Bloom.Core.Models
{
  public class Portfolio
  {
    public required PortfolioSummary Summary { get; set; }
    public List<Position>? Positions { get; set; }
  }
}