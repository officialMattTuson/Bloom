namespace Bloom.Core.Models
{
  public class NewsArticle
  {
    public required string Headline { get; set; }
    public required string Source { get; set; }
    public required string Summary { get; set; }
    public DateTime Timestamp { get; set; }
  }
}