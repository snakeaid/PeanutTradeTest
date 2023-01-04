namespace PeanutTradeTest.Abstractions;

public interface IExchange
{
    public Task<double> GetRate(string inputCurrency, string outputCurrency);
}