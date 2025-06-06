using Microsoft.AspNetCore.Mvc;
using Bloom.Core.Models;

namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/news")]
  public class NewsController : ControllerBase
  {
    [HttpGet("latest")]
    public ActionResult<IEnumerable<NewsArticle>> GetLatestNews()
    {
      var articles = new List<NewsArticle>
            {
                new NewsArticle
                {
                    Headline = "Markets Rally Amid Fed Optimism",
                    Source = "Bloom",
                    Summary = "Stocks rose today after comments from the Federal Reserve...",
                    Timestamp = DateTime.UtcNow.AddMinutes(-15)
                },
                new NewsArticle
                {
                    Headline = "AI Trading Sees Boom",
                    Source = "TechCrunch",
                    Summary = "New AI tools are reshaping retail investing...",
                    Timestamp = DateTime.UtcNow.AddMinutes(-42)
                }
            };

      return Ok(articles);
    }
  }
}