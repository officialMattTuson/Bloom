using Bloom.Core.Models;
using Bloom.Core.Enums;
using Bloom.API.Services.Interfaces;
using Bloom.Persistence.Repositories.Interfaces;
namespace Bloom.API.Services
{
  public class TradingService : ITradingService
  {
    private readonly IPortfolioRepository _portfolioRepo;
    private readonly IIndicatorRuleRepository _ruleRepo;
    private readonly ITickerIndicatorRepository _indicatorRepo;
    private readonly ITradeLogRepository _tradeLogRepo;
    private readonly ITradeExecutionService _tradeExecutionService;

    public TradingService(
        IPortfolioRepository portfolioRepo,
        IIndicatorRuleRepository ruleRepo,
        ITickerIndicatorRepository indicatorRepo,
        ITradeLogRepository tradeLogRepo,
        ITradeExecutionService tradeExecutionService)
    {
      _portfolioRepo = portfolioRepo;
      _ruleRepo = ruleRepo;
      _indicatorRepo = indicatorRepo;
      _tradeLogRepo = tradeLogRepo;
      _tradeExecutionService = tradeExecutionService;
    }

    public async Task<List<RuleEvaluationResult>> EvaluateAndExecuteAsync(string portfolioId)
    {
      var portfolio = await _portfolioRepo.GetByIdAsync(portfolioId);
      var rules = await _ruleRepo.GetEnabledRulesAsync(portfolioId);
      var indicatorData = await _indicatorRepo.GetAllAsync();

      var evaluations = new List<RuleEvaluationResult>();

      foreach (var rule in rules)
      {
        if (!indicatorData.TryGetValue(rule.Symbol, out var data) ||
            !data.Indicators.TryGetValue(rule.Indicator, out var value))
        {
          evaluations.Add(new RuleEvaluationResult
          {
            Symbol = rule.Symbol,
            Rule = rule,
            Passed = false,
            Reason = "Missing indicator data"
          });
          continue;
        }

        var passed = rule.Operator switch
        {
          ">" => value > rule.Threshold,
          "<" => value < rule.Threshold,
          ">=" => value >= rule.Threshold,
          "<=" => value <= rule.Threshold,
          _ => false
        };

        evaluations.Add(new RuleEvaluationResult
        {
          Symbol = rule.Symbol,
          Rule = rule,
          Passed = passed,
          Reason = passed ? null : $"Condition failed: {value} {rule.Operator} {rule.Threshold}"
        });

        if (passed)
        {
          var log = new TradeLog
          {
            PortfolioId = portfolioId,
            Symbol = rule.Symbol,
            Action = rule.Action,
            Price = value,
            Quantity = 1, // For now
            RuleId = rule.Id,
            Notes = "AI rule evaluation"
          };
          await _tradeLogRepo.CreateAsync(log);
          await _tradeExecutionService.ExecuteTradeAsync(log);
        }
      }

      return evaluations;
    }
  }
}