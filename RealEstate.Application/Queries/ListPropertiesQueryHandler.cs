using MediatR;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries
{
    public class ListPropertiesQueryHandler : IRequestHandler<ListPropertiesQuery, IEnumerable<Property>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyFilter _propertyFilter;

        public ListPropertiesQueryHandler(IPropertyRepository propertyRepository, IPropertyFilter propertyFilter)
        {
            _propertyRepository = propertyRepository;
            _propertyFilter = propertyFilter;
        }

        public async Task<IEnumerable<Property>> Handle(ListPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRepository.GetAllAsync();

            return _propertyFilter.Apply(properties, request);
        }
    }
}
