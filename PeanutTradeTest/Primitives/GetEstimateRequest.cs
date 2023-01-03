namespace PeanutTradeTest.Primitives;

public class GetEstimateRequest
{
    public int InputAmount { get; set; }
    public string? InputCurrency { get; set; }
    public string? OutputCurrency { get; set; }
}