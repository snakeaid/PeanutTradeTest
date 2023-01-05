using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PeanutTradeTest.BusinessLogic.Requests;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Handlers;

/// <summary>
/// MediatR handler class which handles the request to get rates on all exchanges and
/// implements <see cref="IRequestHandler{TRequest,TResponse}"/> for
/// <see cref="GetRatesRequest"/>, <see cref="List{T}"/> for <see cref="ExchangeRate"/>.
/// </summary>
public class GetRatesHandler : IRequestHandler<GetRatesRequest, List<ExchangeRate>>
{
    private readonly Market _market;
    private readonly IValidator<GetRatesModel> _validator;
    private readonly ILogger<GetRatesHandler> _logger;

    /// <summary>
    /// Constructs an instance of <see cref="GetRatesHandler"/> using the specified market, validator and logger.
    /// </summary>
    /// <param name="market">An instance of <see cref="Market"/>.</param>
    /// <param name="validator">An instance of <see cref="IValidator{T}"/> for <see cref="GetRatesModel"/>.</param>
    /// <param name="logger">An instance of <see cref="ILogger{TCategoryName}"/> for <see cref="GetRatesHandler"/>.</param>
    public GetRatesHandler(Market market, IValidator<GetRatesModel> validator, ILogger<GetRatesHandler> logger)
    {
        _market = market;
        _validator = validator;
        _logger = logger;
    }

    /// <summary>
    /// Handles the specified request to get rates on all exchanges.
    /// </summary>
    /// <param name="request">Request to get rates on all exchanges.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><see cref="Task{TResult}"/> for <see cref="List{T}"/> for <see cref="ExchangeRate"/>.</returns>
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