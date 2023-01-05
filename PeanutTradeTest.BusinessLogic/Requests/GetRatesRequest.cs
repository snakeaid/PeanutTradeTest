using MediatR;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Requests;

/// <summary>
/// MediatR request class which represents a request to get rates on all exchanges and implements
/// <see cref="IRequest{TResponse}"/> for <see cref="List{T}"/> for <see cref="ExchangeRate"/>.
/// </summary>
public class GetRatesRequest : IRequest<List<ExchangeRate>>
{
    /// <summary>
    /// Gets and sets the model with all information.
    /// </summary>
    public GetRatesModel Model { get; set; } = null!;
}