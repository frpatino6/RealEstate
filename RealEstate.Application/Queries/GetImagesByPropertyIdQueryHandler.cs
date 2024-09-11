using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries
{
    public class GetImagesByPropertyIdQueryHandler : IRequestHandler<GetImagesByPropertyIdQuery, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public GetImagesByPropertyIdQueryHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<IEnumerable<PropertyImage>> Handle(GetImagesByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            return await _propertyImageRepository.GetByPropertyIdAsync(request.IdProperty);
        }
    }
}