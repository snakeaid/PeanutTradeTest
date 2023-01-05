using FluentValidation;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.Validation;

/// <summary>
/// This class provides validation for <see cref="GetEstimateModel"/> and implements
/// <see cref="AbstractValidator{T}"/> for <see cref="GetEstimateModel"/>.
/// </summary>
public class GetEstimateModelValidator : AbstractValidator<GetEstimateModel>
{
    /// <summary>
    /// Constructs an instance of <see cref="GetEstimateModelValidator"/> and defines all validation rules.
    /// </summary>
    public GetEstimateModelValidator()
    {
        RuleFor(x => x.InputAmount).GreaterThan(0);
        RuleFor(x => x.InputCurrency).NotEmpty();
        RuleFor(x => x.OutputCurrency).NotEmpty();
    }
}