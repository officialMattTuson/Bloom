using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Bloom.Core.Enums;

namespace Bloom.Core.Models
{
  public class PortfolioTransaction
  {
    [MongoDB.Bson.Serialization.Attributes.BsonId]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string PortfolioId { get; set; }
    public required string Symbol { get; set; }
    public TradeAction Action { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime ExecutedAt { get; set; }
    public string? RuleId { get; set; }
    public string? TradeLogId { get; set; }
    public decimal? RealizedGain { get; set; }
  }
}