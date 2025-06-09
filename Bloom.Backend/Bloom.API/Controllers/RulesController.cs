using Bloom.API.Services.Interfaces;
using Bloom.Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bloom.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RulesController : ControllerBase
  {
    private readonly IIndicatorRuleService _service;

    public RulesController(IIndicatorRuleService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? symbol = null)
    {
      var rules = await _service.GetAllAsync(symbol);
      return Ok(rules);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
      var rule = await _service.GetByIdAsync(id);
      if (rule == null) return NotFound();
      return Ok(rule);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IndicatorRule rule)
    {
      rule.CreatedAt = DateTime.UtcNow;
      await _service.CreateAsync(rule);
      return CreatedAtAction(nameof(GetById), new { id = rule.Id }, rule);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] IndicatorRule rule)
    {
      var success = await _service.UpdateAsync(id, rule);
      return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      var success = await _service.DeleteAsync(id);
      return success ? NoContent() : NotFound();
    }
  }
}