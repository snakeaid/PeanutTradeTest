using MediatR;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

public class GetRatesHandler : IRequestHandler<GetRatesRequest, List<ExchangeRate>>
{
    private readonly Market _market;

    public GetRatesHandler(Market market)
    {
        _market = market;
    }

    public async Task<List<ExchangeRate>> Handle(GetRatesRequest request, CancellationToken cancellationToken)
    {
        //TODO: Add logging
        List<ExchangeRate> rates = new ();
        foreach (var exchange in _market.Exchanges)
        {
            rates.Add(new ExchangeRate
            {
                ExchangeName = exchange.Item1,
                Rate = await exchange.Item2.GetRate(request.BaseCurrency!, request.QuoteCurrency!)
            });
        }

        return rates;
    }
}