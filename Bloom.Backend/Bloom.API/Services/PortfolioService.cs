using Bloom.API.Services.Interfaces;
using Bloom.Core.Models;

namespace Bloom.API.Services
{
    public class PortfolioService : IPortfolioService
    {
        public Portfolio BuildPortfolio()
        {
            var positions = GetMockPositions();

            var totalValue = positions.Sum(p => p.Value);
            var totalCost = positions.Sum(p => p.CostBasis);
            var totalReturn = positions.Sum(p => p.UnrealizedGain);
            var avgReturn = positions.Average(p => p.ReturnPercent);

            var summary = new PortfolioSummary
            {
                TotalValue = totalValue,
                DailyChange = 1250.5m, // Optional, could be calculated later
                DailyChangePercent = 2.56m,
                TotalReturn = totalReturn,
                TotalReturnPercent = totalCost == 0 ? 0 : (totalReturn / totalCost) * 100,
                Positions = positions.Count,
                AverageReturn = Math.Round(avgReturn, 2)
            };

            return new Portfolio
            {
                Summary = summary,
                Positions = positions
            };
        }

        private static List<Position> GetMockPositions() => new()
        {
            new() { Symbol = "AAPL", Quantity = 100, AveragePrice = 175.5m, CurrentPrice = 185.25m },
            new() { Symbol = "GOOGL", Quantity = 15, AveragePrice = 2750m, CurrentPrice = 2820.5m },
            new() { Symbol = "MSFT", Quantity = 50, AveragePrice = 365.75m, CurrentPrice = 378.9m },
            new() { Symbol = "NVDA", Quantity = 25, AveragePrice = 380.25m, CurrentPrice = 425.8m },
            new() { Symbol = "TSLA", Quantity = 30, AveragePrice = 265.5m, CurrentPrice = 248.5m }
        };
    }
}
