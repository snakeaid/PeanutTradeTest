using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

/// <summary>
/// MediatR handler class which handles the request to get an estimate and
/// implements <see cref="IRequestHandler{TRequest,TResponse}"/> for
/// <see cref="GetEstimateRequest"/>, <see cref="Estimate"/>.
/// </summary>
public class GetEstimateHandler : IRequestHandler<GetEstimateRequest, Estimate>
{
    private readonly Market _market;
    private readonly IValidator<GetEstimateModel> _validator;
    private readonly ILogger<GetEstimateHandler> _logger;

    /// <summary>
    /// Constructs an instance of <see cref="GetEstimateHandler"/> using the specified market, validator and logger.
    /// </summary>
    /// <param name="market">An instance of <see cref="Market"/>.</param>
    /// <param name="validator">An instance of <see cref="IValidator{T}"/> for <see cref="GetEstimateModel"/>.</param>
    /// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> for <see cref="GetEstimateHandler"/>.</param>
    public GetEstimateHandler(Market market, IValidator<GetEstimateModel> validator, ILogger<GetEstimateHandler> logger)
    {
        _market = market;
        _validator = validator;
        _logger = logger;
    }

    /// <summary>
    /// Handles the specified request to get an estimate.
    /// </summary>
    /// <param name="request">Request to get an estimate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><see cref="Task{TResult}"/> for <see cref="Estimate"/>.</returns>
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