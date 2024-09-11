using FluentValidation;
using RealEstate.Application.Commands;

namespace RealEstate.Application.Validators
{
    public class CreateOwnerValidator : AbstractValidator<CreateOwnerCommand>
    {
        public CreateOwnerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Owner name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Owner address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Owner photo is required.");

            RuleFor(x => x.Birthday)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Birthday cannot be in the future.");
        }
    }
}
