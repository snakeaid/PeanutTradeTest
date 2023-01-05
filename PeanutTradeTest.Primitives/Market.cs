using System.Collections;
using PeanutTradeTest.Abstractions;

namespace PeanutTradeTest.Primitives;

/// <summary>
/// This class represents the crypto currency market.
/// </summary>
public class Market
{
    /// <summary>
    /// Gets the list of all exchanges paired with their names.
    /// </summary>
    public List<(string, IExchange)> Exchanges { get; }

    /// <summary>
    /// Constructs an instance of <see cref="Market"/>.
    /// </summary>
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