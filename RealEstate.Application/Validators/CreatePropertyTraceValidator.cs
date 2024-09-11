using FluentValidation;
using RealEstate.Application.Commands;

namespace RealEstate.Application.Validators
{
    public class CreatePropertyTraceValidator : AbstractValidator<AddPropertyTraceCommand>
    {
        public CreatePropertyTraceValidator()
        {
            RuleFor(x => x.IdProperty)
                .NotEmpty().WithMessage("Property ID is required.");

            RuleFor(x => x.DateSale)
                .NotEmpty().WithMessage("Sale date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Sale date cannot be in the future.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Value must be greater than 0.");

            RuleFor(x => x.Tax)
                .GreaterThanOrEqualTo(0).WithMessage("Tax cannot be negative.");
        }
    }
}

