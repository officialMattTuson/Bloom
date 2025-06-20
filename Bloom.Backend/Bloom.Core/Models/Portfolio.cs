using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bloom.Core.Models
{
  public class Portfolio
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public required PortfolioSummary Summary { get; set; }
    public List<Position>? Positions { get; set; }
    public decimal InitialCapital { get; set; }

    public decimal CashBalance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
  }
}