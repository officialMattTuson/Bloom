using Microsoft.AspNetCore.Mvc;
using Bloom.Core.Models;
using Bloom.API.Services.Interfaces;

namespace Bloom.API.Controllers
{

  [ApiController]
  [Route("api/portfolio")]
  public class PortfolioController : ControllerBase
  {
    private readonly IPortfolioService _portfolioService;
    public PortfolioController(IPortfolioService portfolioService)
    {
      _portfolioService = portfolioService;
    }

    [HttpGet("summary")]
    public ActionResult<Portfolio> GetPortfolio()
    {
      var summary = _portfolioService.BuildPortfolio();

      return Ok(summary);
    }

  }
}