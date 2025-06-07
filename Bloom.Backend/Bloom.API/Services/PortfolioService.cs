using Bloom.Core.Models;
using Bloom.Persistence.Repositories.Interfaces;
using Bloom.API.Services.Interfaces;

namespace Bloom.API.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<List<Portfolio>> GetAllAsync()
        {
            return await _portfolioRepository.GetAllAsync();
        }

        public async Task CreateAsync(Portfolio portfolio)
        {
            var summary = CalculateSummary(portfolio.Positions ?? []);
            portfolio.Summary = summary;

            await _portfolioRepository.InsertAsync(portfolio);
        }

        private static PortfolioSummary CalculateSummary(List<Position> positions)
        {
            if (!positions.Any())
                return new PortfolioSummary();

            var totalValue = positions.Sum(p => p.Value);
            var totalCost = positions.Sum(p => p.CostBasis);
            var totalReturn = totalValue - totalCost;
            var returnPercent = totalCost == 0 ? 0 : Math.Round(totalReturn / totalCost * 100, 2);

            return new PortfolioSummary
            {
                TotalValue = totalValue,
                DailyChange = 0, // You can enhance this later with price history
                DailyChangePercent = 0,
                TotalReturn = totalReturn,
                TotalReturnPercent = returnPercent,
                Positions = positions.Count,
                AverageReturn = Math.Round(positions.Average(p => p.ReturnPercent), 2)
            };
        }
    }
}
