namespace PeanutTradeTest.Abstractions;

/// <summary>
/// This interface represents a cryptocurrency exchange.
/// </summary>
public interface IExchange
{
    /// <summary>
    /// Gets the rate for the specified pair.
    /// </summary>
    /// <param name="inputCurrency">The input currency.</param>
    /// <param name="outputCurrency">The output currency.</param>
    /// <returns>The rate.</returns>
    public Task<double> GetRate(string inputCurrency, string outputCurrency);
}