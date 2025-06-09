using Bloom.Core.Enums;

namespace Bloom.Core.Models
{
  public class TradeLogQueryRequest
  {
    public string? PortfolioId { get; set; }
    public string? Symbol { get; set; }
    public TradeAction? Action { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 50;
    public string SortField { get; set; } = "ExecutedAt";
    public int SortOrder { get; set; } = -1; // -1 = Desc, 1 = Asc
  }
}