using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

public class GetEstimateHandler : IRequestHandler<GetEstimateRequest, Estimate>
{
    private readonly Market _market;
    private readonly IValidator<GetEstimateModel> _validator;
    private readonly ILogger<GetEstimateHandler> _logger;

    public GetEstimateHandler(Market market, IValidator<GetEstimateModel> validator, ILogger<GetEstimateHandler> logger)
    {
        _market = market;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Estimate> Handle(GetEstimateRequest request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        await _validator.ValidateAndThrowAsync(model, cancellationToken);
        
        _logger.LogInformation("Getting the best rate");
        
        ExchangeRate bestRate = new ExchangeRate { Rate = -1 };

        foreach (var exchange in _market.Exchanges)
        {
            var currentRate = new ExchangeRate
            {
                ExchangeName = exchange.Item1,
                Rate = await exchange.Item2.GetRate(model.InputCurrency!, model.OutputCurrency!)
            };
            if (currentRate.Rate > bestRate.Rate) bestRate = currentRate;
        }
        
        _logger.LogInformation("Got the best rate successfully, calculating the output amount.");

        return new Estimate
            { ExhcangeName = bestRate.ExchangeName, OutputAmount = model.InputAmount * bestRate.Rate };
    }
}