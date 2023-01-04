using MediatR;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Requests;

public class GetRatesRequest : IRequest<List<ExchangeRate>>
{
    public GetRatesModel Model { get; set; }
}