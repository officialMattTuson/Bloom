namespace Bloom.Core.Models;

public class CreatePortfolioRequest
{
  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public decimal InitialCapital { get; set; }
  public List<Position> Positions { get; set; } = new();
}
