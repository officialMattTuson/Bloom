namespace Bloom.Core.Models;
public class CreatePortfolioRequest
{
  public List<Position> Positions { get; set; } = new();
}
