using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyRepository _propertyRepository;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = new Property
            {
                IdProperty = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                CodeInternal = request.CodeInternal,
                Year = request.Year,
                Address = request.Address,
                IdOwner = request.IdOwner
            };

            await _propertyRepository.AddAsync(property);

            return property.IdProperty;
        }
    }
}