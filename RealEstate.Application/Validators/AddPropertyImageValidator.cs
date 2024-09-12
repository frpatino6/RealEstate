using FluentValidation;
using RealEstate.Application.Commands;

namespace RealEstate.Application.Validators
{
    public class AddPropertyImageValidator : AbstractValidator<AddPropertyImageCommand>
    {
        public AddPropertyImageValidator()
        {
            RuleFor(x => x.IdProperty)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.File)
                .NotEmpty().WithMessage("Image file is required.");

            RuleFor(x => x.Enabled)
                .NotNull().WithMessage("Image status (enabled or disabled) is required.");

            RuleFor(x => x.Location)
                .NotNull().WithMessage("Image status (enabled or disabled) is required.");


        }
    }
}
