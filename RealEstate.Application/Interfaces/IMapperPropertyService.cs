using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

public interface IMapperPropertyService
{
    PropertyDto MapToDto(Property property);
    IEnumerable<PropertyDto> MapToDto(IEnumerable<Property> properties);
}
