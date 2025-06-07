using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Bloom.Core.Enums;

namespace Bloom.Core.Models
{
  public class IndicatorRule
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string Symbol { get; set; } 

    public required string Indicator { get; set; }

    public required string Operator { get; set; }

    public decimal Threshold { get; set; }

    public TradeAction Action { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  }
}