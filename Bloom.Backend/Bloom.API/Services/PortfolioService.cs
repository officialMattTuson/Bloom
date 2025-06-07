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

        public async Task<Portfolio?> GetByIdAsync(string id)
        {
            return await _portfolioRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Portfolio portfolio)
        {
            var summary = CalculateSummary(portfolio.Positions ?? []);
            portfolio.Summary = summary;

            await _portfolioRepository.CreateAsync(portfolio);
        }

        public async Task<bool> UpdateAsync(string id, Portfolio updatedPortfolio)
        {
            var summary = CalculateSummary(updatedPortfolio.Positions ?? []);
            updatedPortfolio.Summary = summary;

            return await _portfolioRepository.UpdateAsync(id, updatedPortfolio);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _portfolioRepository.DeleteAsync(id);
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
                DailyChange = 0,
                DailyChangePercent = 0,
                TotalReturn = totalReturn,
                TotalReturnPercent = returnPercent,
                Positions = positions.Count,
                AverageReturn = Math.Round(positions.Average(p => p.ReturnPercent), 2)
            };
        }
    }
}
