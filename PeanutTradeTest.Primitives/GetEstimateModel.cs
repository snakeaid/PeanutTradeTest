namespace PeanutTradeTest.Primitives;

/// <summary>
/// This class encapsulates all information for the request to get an estimate. 
/// </summary>
public class GetEstimateModel
{
    /// <summary>
    /// Gets and sets the input amount.
    /// </summary>
    public double InputAmount { get; set; }
    
    /// <summary>
    /// Gets and sets the input currency.
    /// </summary>
    public string? InputCurrency { get; set; }
    
    /// <summary>
    /// Gets and sets the output currency.
    /// </summary>
    public string? OutputCurrency { get; set; }
}