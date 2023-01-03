using Microsoft.AspNetCore.Mvc;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketController : ControllerBase
{
    [HttpGet("estimate/")]
    public IActionResult GetEstimate(GetEstimateRequest request)
    {
        return null;
    }
    
    [HttpGet("rates/")]
    public IActionResult GetRates(GetRatesRequest request)
    {
        return null;
    }
}