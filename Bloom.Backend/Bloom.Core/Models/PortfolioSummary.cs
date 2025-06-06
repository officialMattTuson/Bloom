namespace Bloom.Core.Models
{
  public class PortfolioSummary 
  {
    public decimal TotalValue { get; set; }
    public decimal DailyChange { get; set; }
    public decimal DailyChangePercent { get; set; }
    public decimal TotalReturn { get; set; }
    public decimal TotalReturnPercent { get; set; }
    public decimal Positions { get; set; }
    public decimal AverageReturn { get; set; }
  }
}