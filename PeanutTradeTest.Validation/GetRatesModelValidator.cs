using FluentValidation;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.Validation;

/// <summary>
/// This class provides validation for <see cref="GetRatesModel"/> and implements
/// <see cref="AbstractValidator{T}"/> for <see cref="GetRatesModel"/>.
/// </summary>
public class GetRatesModelValidator : AbstractValidator<GetRatesModel>
{
    /// <summary>
    /// Constructs an instance of <see cref="GetRatesModelValidator"/> and defines all validation rules.
    /// </summary>
    public GetRatesModelValidator()
    {
        RuleFor(x => x.BaseCurrency).NotEmpty();
        RuleFor(x => x.QuoteCurrency).NotEmpty();
    }
}