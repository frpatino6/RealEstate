using MediatR;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Queries
{
    public class ListPropertyWithFiltersQueryHandler : IRequestHandler<ListPropertyWithFiltersQuery, IEnumerable<RealEstate.Domain.Entities.Property>>
    {
        private readonly IPropertyRepository _propertyRepository;

        public ListPropertyWithFiltersQueryHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Property>> Handle(ListPropertyWithFiltersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Property> properties = await _propertyRepository.GetAllAsync();

            if (request.MinPrice.HasValue)
            {
                properties = properties.Where(p => p.Price >= request.MinPrice.Value);
            }
            if (request.MaxPrice.HasValue)
            {
                properties = properties.Where(p => p.Price <= request.MaxPrice.Value);
            }

            return properties;
        }
    }
}