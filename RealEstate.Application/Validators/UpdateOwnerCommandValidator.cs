using FluentValidation;
using RealEstate.Application.Commands;

namespace RealEstate.Application.Validators
{
    public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
    {
        public UpdateOwnerCommandValidator()
        {
            RuleFor(x => x.IdOwner)
                .NotEmpty().WithMessage("Owner ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Photo is required.");

            RuleFor(x => x.Birthday)
                .LessThan(DateTime.Now).WithMessage("Birthday must be a past date.");
        }
    }
}


