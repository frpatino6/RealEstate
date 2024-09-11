using MediatR;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands
{
    public class ChangePriceCommandHandler : IRequestHandler<ChangePriceCommand>
    {
        private readonly IPropertyRepository _propertyRepository;

        public ChangePriceCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Unit> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(request.IdProperty);
            if (property == null)
            {
                throw new Exception("Property not found");
            }

            property.Price = request.NewPrice;
            await _propertyRepository.UpdateAsync(property);

            return Unit.Value;
        }
    }
}