using MediatR;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand>
    {
        private readonly IPropertyRepository _propertyRepository;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(request.IdProperty);
            if (property is null)
            {
                throw new Exception("Property not found");
            }
            property.IdProperty = request.IdProperty;
            property.Name = request.Name;
            property.Price = request.Price;
            property.Address = request.Address;
            property.Year = request.Year;

            await _propertyRepository.UpdateAsync(property);

            return Unit.Value;
        }
    }
}