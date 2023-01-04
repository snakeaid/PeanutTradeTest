using System.Collections;
using PeanutTradeTest.Abstractions;

namespace PeanutTradeTest.Primitives;

public class Market
{
    public List<(string, IExchange)> Exchanges { get; }

    public Market()
    {
        Exchanges = new();
        foreach (var type in typeof(Market).Assembly.GetTypes())
        {
            if (typeof(IExchange).IsAssignableFrom(type)) 
                Exchanges.Add((type.Name.Replace("Exchange", string.Empty).ToLower(), 
                    (IExchange)Activator.CreateInstance(type)!));
        }
    }
}