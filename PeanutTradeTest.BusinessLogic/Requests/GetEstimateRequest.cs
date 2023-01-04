using MediatR;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Requests;

public class GetEstimateRequest : IRequest<Estimate>
{
    public GetEstimateModel Model { get; set; }
}