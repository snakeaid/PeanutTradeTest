using System.Globalization;
using System.Text.Json;
using PeanutTradeTest.Abstractions;

namespace PeanutTradeTest.Primitives;

/// <summary>
/// This class represents the Kucoin exchange and implements <see cref="IExchange"/>.
/// </summary>
public class KucoinExchange : IExchange
{
    /// <summary>
    /// Gets the rate for the specified pair.
    /// </summary>
    /// <param name="inputCurrency">The input currency.</param>
    /// <param name="outputCurrency">The output currency.</param>
    /// <returns><see cref="Task"/> for <see cref="double"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if there is no such trading pair.</exception>
    public async Task<double> GetRate(string inputCurrency, string outputCurrency)
    {
        var client = new HttpClient {BaseAddress = new Uri("https://api.kucoin.com")};
        var endpoint = $"/api/v1/market/stats?symbol={inputCurrency}-{outputCurrency}";
        var response = await client.GetStringAsync(endpoint);
        var last = JsonDocument.Parse(response).RootElement.GetProperty("data").GetProperty("last").GetString();

        if (last != null) return Double.Parse(last, CultureInfo.InvariantCulture);
        
        endpoint = $"/api/v1/market/stats?symbol={outputCurrency}-{inputCurrency}";
        response = await client.GetStringAsync(endpoint);
        last = JsonDocument.Parse(response).RootElement.GetProperty("data").GetProperty("last").GetString();

        if (last == null) throw new ArgumentException("No such trading pair on Kucoin.");
        return 1 / Double.Parse(last, CultureInfo.InvariantCulture);
    }
}