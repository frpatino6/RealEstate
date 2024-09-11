using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Commands
{
    public class AddPropertyImageCommandHandler : IRequestHandler<AddPropertyImageCommand, Guid>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public AddPropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<Guid> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
        {
            var image = new PropertyImage
            {
                IdPropertyImage = Guid.NewGuid(),
                IdProperty = request.IdProperty,
                File = request.File,
                Enabled = request.Enabled
            };

            await _propertyImageRepository.AddAsync(image);

            return image.IdPropertyImage;
        }
    }
}