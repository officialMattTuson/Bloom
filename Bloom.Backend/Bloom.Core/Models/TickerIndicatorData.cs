using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bloom.Core.Models
{
  public class TickerIndicatorData
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string Symbol { get; set; }

    public Dictionary<string, decimal> Indicators { get; set; } = new(); // e.g., {"RSI": 28.7m}

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
  }
}