using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

public class ListPropertyWithFiltersQueryHandler : IRequestHandler<ListPropertyWithFiltersQuery, IEnumerable<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPropertyFilter _propertyFilter;
    private readonly IMapperPropertyService _mapperPropertyService;

    public ListPropertyWithFiltersQueryHandler(
        IPropertyRepository propertyRepository,
        IPropertyFilter propertyFilter,
        IMapperPropertyService mapperPropertyService)
    {
        _propertyRepository = propertyRepository;
        _propertyFilter = propertyFilter;
        _mapperPropertyService = mapperPropertyService;
    }

    public async Task<IEnumerable<PropertyDto>> Handle(ListPropertyWithFiltersQuery request, CancellationToken cancellationToken)
    {
        var properties = await _propertyRepository.GetAllAsync();

        var filteredProperties = _propertyFilter.Apply(properties, request);

        return filteredProperties.Select(MapToDto);
    }

    private PropertyDto MapToDto(Property property)
    {
        return _mapperPropertyService.MapToDto(property);
    }
}
