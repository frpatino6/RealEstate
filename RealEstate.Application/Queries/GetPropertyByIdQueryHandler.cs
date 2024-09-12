using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Queries
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapperPropertyService _mapperPropertyService;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapperPropertyService mapperPropertyService)
        {
            _propertyRepository = propertyRepository;
            _mapperPropertyService = mapperPropertyService;
        }

        public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(request.Id);

            return _mapperPropertyService.MapToDto(property);
        }
    }
}
