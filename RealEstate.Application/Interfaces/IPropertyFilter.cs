using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces
{
    public interface IPropertyFilter
    {
        IEnumerable<Property> Apply(IEnumerable<Property> properties, ListPropertiesQuery query);
        IEnumerable<Property> Apply(IEnumerable<Property> properties, ListPropertyWithFiltersQuery query);
    }
}
