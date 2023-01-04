using FluentValidation;
using MediatR;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

public class GetEstimateHandler : IRequestHandler<GetEstimateRequest, Estimate>
{
    private readonly Market _market;
    private readonly IValidator<GetEstimateModel> _validator;

    public GetEstimateHandler(Market market, IValidator<GetEstimateModel> validator)
    {
        _market = market;
        _validator = validator;
    }

    public async Task<Estimate> Handle(GetEstimateRequest request, CancellationToken cancellationToken)
    {
        //TODO: Add logging
        var model = request.Model;
        await _validator.ValidateAndThrowAsync(model, cancellationToken);
        
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

        return new Estimate
            { ExhcangeName = bestRate.ExchangeName, OutputAmount = model.InputAmount * bestRate.Rate };
    }
}