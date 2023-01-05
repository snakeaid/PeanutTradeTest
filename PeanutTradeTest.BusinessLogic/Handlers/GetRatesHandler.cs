using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

public class GetRatesHandler : IRequestHandler<GetRatesRequest, List<ExchangeRate>>
{
    private readonly Market _market;
    private readonly IValidator<GetRatesModel> _validator;
    private readonly ILogger<GetRatesHandler> _logger;

    public GetRatesHandler(Market market, IValidator<GetRatesModel> validator, ILogger<GetRatesHandler> logger)
    {
        _market = market;
        _validator = validator;
        _logger = logger;
    }

    public async Task<List<ExchangeRate>> Handle(GetRatesRequest request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        await _validator.ValidateAndThrowAsync(model, cancellationToken);
        
        _logger.LogInformation("Getting all rates.");
        
        List<ExchangeRate> rates = new ();
        foreach (var exchange in _market.Exchanges)
        {
            rates.Add(new ExchangeRate
            {
                ExchangeName = exchange.Item1,
                Rate = await exchange.Item2.GetRate(model.BaseCurrency!, model.QuoteCurrency!)
            });
        }
        
        _logger.LogInformation("Got all rates successfully.");

        return rates;
    }
}