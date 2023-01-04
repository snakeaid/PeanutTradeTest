using MediatR;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.BusinessLogic.Requests;

public class GetRatesRequest : IRequest<List<ExchangeRate>>
{
    public string? BaseCurrency { get; set; }
    public string? QuoteCurrency { get; set; }
}