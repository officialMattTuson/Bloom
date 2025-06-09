namespace Bloom.Core.Models;

public class CreatePortfolioRequest
{
  public decimal InitialCapital { get; set; }
  public List<Position> Positions { get; set; } = new();
}
