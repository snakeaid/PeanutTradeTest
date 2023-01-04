using FluentValidation;
using PeanutTradeTest.Primitives;

namespace PeanutTradeTest.Validation;

public class GetRatesModelValidator : AbstractValidator<GetRatesModel>
{
    public GetRatesModelValidator()
    {
        RuleFor(x => x.BaseCurrency).NotEmpty();
        RuleFor(x => x.QuoteCurrency).NotEmpty();
    }
}