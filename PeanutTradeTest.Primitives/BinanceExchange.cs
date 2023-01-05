using System.Globalization;
using System.Text.Json;
using PeanutTradeTest.Abstractions;

namespace PeanutTradeTest.Primitives;

/// <summary>
/// This class represents the Binance exchange and implements <see cref="IExchange"/>.
/// </summary>
public class BinanceExchange : IExchange
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
        string? last;
        var client = new HttpClient {BaseAddress = new Uri("https://api.binance.com")};
        var endpoint = $"/api/v3/ticker/price?symbol={inputCurrency}{outputCurrency}";
        var response = await client.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            last = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement.GetProperty("price").GetString();
            return Double.Parse(last!, CultureInfo.InvariantCulture);
        }

        endpoint = $"/api/v3/ticker/price?symbol={outputCurrency}{inputCurrency}";
        response = await client.GetAsync(endpoint);
        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException("No such trading pair on Binance.");
        }
        last = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement.GetProperty("price").GetString();
        return 1 / Double.Parse(last!, CultureInfo.InvariantCulture);
    }
}