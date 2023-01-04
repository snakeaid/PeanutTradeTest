using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.API.Controllers;

[ApiController]
[Route("api/")]
public class MarketController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("estimate/")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Estimate))]
    public async Task<IActionResult> GetEstimate(GetEstimateModel model)
    {
        var result = await _mediator.Send(new GetEstimateRequest { Model = model });
        return Ok(result);
    }
    
    [HttpPost("rates/")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ExchangeRate>))]
    public async Task<IActionResult> GetRates(GetRatesModel model)
    {
        var result = await _mediator.Send(new GetRatesRequest { Model = model });
        return Ok(result);
    }
}