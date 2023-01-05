using MediatR;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Requests;

/// <summary>
/// MediatR request class which represents a request to get an estimate and implements
/// <see cref="IRequest{TResponse}"/> for <see cref="Estimate"/>.
/// </summary>
public class GetEstimateRequest : IRequest<Estimate>
{
    /// <summary>
    /// Gets and sets the model with all information.
    /// </summary>
    public GetEstimateModel Model { get; set; } = null!;
}