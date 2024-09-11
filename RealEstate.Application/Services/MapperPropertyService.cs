using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

public class MapperPropertyService : IMapperPropertyService
{
    public PropertyDto MapToDto(Property property)
    {
        return new PropertyDto(
            IdProperty: property.IdProperty,
            Name: property.Name,
            Address: property.Address,
            Price: property.Price,
            Year: property.Year
        );
    }

    public IEnumerable<PropertyDto> MapToDto(IEnumerable<Property> properties)
    {
        return properties.Select(property => new PropertyDto(
            IdProperty: property.IdProperty,
            Name: property.Name,
            Address: property.Address,
            Price: property.Price,
            Year: property.Year
         ));
    }
}

