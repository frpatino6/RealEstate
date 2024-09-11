using FluentValidation;
using RealEstate.Application.Commands;

namespace RealEstate.Application.Validators
{
    public class ChangePriceValidator : AbstractValidator<ChangePriceCommand>
    {
        public ChangePriceValidator()
        {
            RuleFor(x => x.IdProperty)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.NewPrice)
                .GreaterThan(0).WithMessage("The new price must be greater than 0.");
        }
    }
}

