namespace Bloom.Core.Models
{
    public class Position
    {
        public required string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }

        public decimal Value => Quantity * CurrentPrice;
        public decimal CostBasis => Quantity * AveragePrice;
        public decimal UnrealizedGain => Value - CostBasis;
        public decimal ReturnPercent => AveragePrice == 0 ? 0 : Math.Round(((CurrentPrice - AveragePrice) / AveragePrice * 100), 2);
    }
}
