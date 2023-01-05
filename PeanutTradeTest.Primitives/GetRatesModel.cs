namespace PeanutTradeTest.Primitives;

/// <summary>
/// This class encapsulates all information for the request to get rates for some pair on all exchanges. 
/// </summary>
public class GetRatesModel
{
    /// <summary>
    /// Gets and sets the base currency.
    /// </summary>
    public string? BaseCurrency { get; set; }
    
    /// <summary>
    /// Gets and sets the quote currency.
    /// </summary>
    public string? QuoteCurrency { get; set; }
}