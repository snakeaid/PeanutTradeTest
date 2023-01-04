using FluentValidation;
using MediatR;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

public class GetRatesHandler : IRequestHandler<GetRatesRequest, List<ExchangeRate>>
{
    private readonly Market _market;
    private readonly IValidator<GetRatesModel> _validator;

    public GetRatesHandler(Market market, IValidator<GetRatesModel> validator)
    {
        _market = market;
        _validator = validator;
    }

    public async Task<List<ExchangeRate>> Handle(GetRatesRequest request, CancellationToken cancellationToken)
    {
        //TODO: Add logging
        var model = request.Model;
        await _validator.ValidateAndThrowAsync(model, cancellationToken);
        
        List<ExchangeRate> rates = new ();
        foreach (var exchange in _market.Exchanges)
        {
            rates.Add(new ExchangeRate
            {
                ExchangeName = exchange.Item1,
                Rate = await exchange.Item2.GetRate(model.BaseCurrency!, model.QuoteCurrency!)
            });
        }

        return rates;
    }
}