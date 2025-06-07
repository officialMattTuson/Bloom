using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bloom.Core.Models
{
  public class TradeLog
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string PortfolioId { get; set; }

    public required string Symbol { get; set; }

    public TradeAction Action { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    public string? RuleId { get; set; }

    public string? Notes { get; set; }
  }
}