using System.Globalization;
using System.Text.Json;
using PeanutTradeTest.Abstractions;

namespace PeanutTradeTest.Primitives;

public class KucoinExchange : IExchange
{
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

        if (last == null) throw new ArgumentException("No such trading pair.");
        return 1 / Double.Parse(last, CultureInfo.InvariantCulture);
    }
}