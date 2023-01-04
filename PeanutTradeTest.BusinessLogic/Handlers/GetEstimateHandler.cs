using MediatR;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

public class GetEstimateHandler : IRequestHandler<GetEstimateRequest, Estimate>
{
    private readonly Market _market;

    public GetEstimateHandler(Market market)
    {
        _market = market;
    }

    public async Task<Estimate> Handle(GetEstimateRequest request, CancellationToken cancellationToken)
    {
        //TODO: Add logging
        ExchangeRate bestRate = new ExchangeRate { Rate = -1 };
        ExchangeRate currentRate;

        foreach (var exchange in _market.Exchanges)
        {
            currentRate = new ExchangeRate
            {
                ExchangeName = exchange.Item1,
                Rate = await exchange.Item2.GetRate(request.InputCurrency!, request.OutputCurrency!)
            };
            if (currentRate.Rate > bestRate.Rate) bestRate = currentRate;
        }

        return new Estimate
            { ExhcangeName = bestRate.ExchangeName, OutputAmount = request.InputAmount * bestRate.Rate };
    }
}