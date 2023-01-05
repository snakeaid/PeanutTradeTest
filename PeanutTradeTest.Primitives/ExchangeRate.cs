namespace PeanutTradeTest.Primitives;

/// <summary>
/// This class represents a rate for a trading pair on an exchange .
/// </summary>
public class ExchangeRate
{
    /// <summary>
    /// Gets and sets the exchange name.
    /// </summary>
    public string ExchangeName { get; set; } = null!;
    
    /// <summary>
    /// Gets and sets the rate.
    /// </summary>
    public double Rate { get; set; }
}