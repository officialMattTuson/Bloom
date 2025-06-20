using Bloom.Core.Models;
using Bloom.Core.Enums;
using Bloom.Persistence.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bloom.API.Services.Interfaces;

namespace Bloom.API.Services
{
  public class TradeExecutionService : ITradeExecutionService
  {
    private readonly IPortfolioRepository _portfolioRepo;
    private readonly IPortfolioTransactionRepository _transactionRepo;

    public TradeExecutionService(IPortfolioRepository portfolioRepo, IPortfolioTransactionRepository transactionRepo)
    {
      _portfolioRepo = portfolioRepo;
      _transactionRepo = transactionRepo;
    }

    public async Task ExecuteTradeAsync(TradeLog log)
    {
      var portfolio = await _portfolioRepo.GetByIdAsync(log.PortfolioId);
      if (portfolio == null) throw new InvalidOperationException("Portfolio not found");

      var positions = portfolio.Positions ?? new List<Position>();
      var position = positions.FirstOrDefault(p => p.Symbol == log.Symbol);

      decimal realizedGain = 0;
      decimal totalCost = log.Price * log.Quantity;

      if (log.Action == TradeAction.Buy)
      {
        if (portfolio.CashBalance < totalCost)
          throw new InvalidOperationException("Insufficient cash to buy");

        if (position == null)
        {
          position = new Position
          {
            Symbol = log.Symbol,
            Quantity = log.Quantity,
            AveragePrice = log.Price,
            CurrentPrice = log.Price
          };
          positions.Add(position);
        }
        else
        {
          var newCost = position.AveragePrice * position.Quantity + totalCost;
          position.Quantity += log.Quantity;
          position.AveragePrice = newCost / position.Quantity;
          position.CurrentPrice = log.Price;
        }

        portfolio.CashBalance -= totalCost;
      }
      else if (log.Action == TradeAction.Sell)
      {
        if (position == null || position.Quantity < log.Quantity)
          throw new InvalidOperationException("Not enough shares to sell");

        realizedGain = (log.Price - position.AveragePrice) * log.Quantity;
        position.Quantity -= log.Quantity;
        if (position.Quantity == 0)
        {
          positions.Remove(position);
        }

        portfolio.Summary.RealizedGainTotal += realizedGain;
        portfolio.Summary.RealizedGainPercent = portfolio.Summary.TotalValue > 0
            ? Math.Round(portfolio.Summary.RealizedGainTotal / portfolio.Summary.TotalValue * 100, 2)
            : 0;

        portfolio.CashBalance += totalCost;
      }

      portfolio.Positions = positions;
      portfolio.Summary.TotalValue = positions.Sum(p => p.Value);
      portfolio.Summary.TotalReturn = positions.Sum(p => p.UnrealizedGain);
      portfolio.Summary.TotalReturnPercent = positions.Count > 0
          ? Math.Round(positions.Average(p => p.ReturnPercent), 2)
          : 0;
      portfolio.Summary.AverageReturn = portfolio.Summary.TotalReturnPercent;
      portfolio.Summary.Positions = positions.Count;

      await _portfolioRepo.UpdateAsync(portfolio.Id!, portfolio);

      var transaction = new PortfolioTransaction
      {
        PortfolioId = log.PortfolioId,
        Symbol = log.Symbol,
        Action = log.Action,
        Price = log.Price,
        Quantity = log.Quantity,
        ExecutedAt = log.ExecutedAt,
        RuleId = log.RuleId,
        TradeLogId = log.Id,
        RealizedGain = realizedGain
      };

      await _transactionRepo.CreateAsync(transaction);
    }
  }
}