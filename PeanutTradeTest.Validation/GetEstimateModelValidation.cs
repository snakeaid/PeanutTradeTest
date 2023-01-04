using FluentValidation;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.Validation;

public class GetEstimateModelValidation : AbstractValidator<GetEstimateModel>
{
    public GetEstimateModelValidation()
    {
        RuleFor(x => x.InputAmount).GreaterThan(0);
        RuleFor(x => x.InputCurrency).NotEmpty();
        RuleFor(x => x.OutputCurrency).NotEmpty();
    }
}