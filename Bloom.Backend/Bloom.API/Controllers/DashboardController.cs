using Microsoft.AspNetCore.Mvc;
using Bloom.Core.Models;

namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/dashboard")]
  public class DashboardController : ControllerBase
  {
    [HttpGet("summary")]
    public ActionResult<DashboardSummary> GetSummary()
    {
      var summary = new DashboardSummary
      {
        TotalValue = 15230.55m,
        DailyChange = 1.12m,
        ProfitLoss = 320.00m
      };

      return Ok(summary);
    }
  }
}