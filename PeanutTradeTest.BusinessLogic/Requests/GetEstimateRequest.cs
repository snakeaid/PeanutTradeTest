using MediatR;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Requests;

public class GetEstimateRequest : IRequest<Estimate>
{
    public double InputAmount { get; set; }
    public string? InputCurrency { get; set; }
    public string? OutputCurrency { get; set; }
}