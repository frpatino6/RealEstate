using FluentValidation;
using RealEstate.Application.Commands;

namespace RealEstate.Application.Validators
{
    public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyValidator()
        {
            RuleFor(x => x.IdProperty)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Property name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.");
        }
    }
}



