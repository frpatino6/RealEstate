using Azure.Core;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Queries;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services
{
    public class PropertyFilter : IPropertyFilter
    {
        public IEnumerable<Property> Apply(IEnumerable<Property> properties, ListPropertiesQuery query)
        {
            var filters = new List<Func<Property, bool>>();

            if (!string.IsNullOrEmpty(query.Address))
            {
                filters.Add(p => p.Address.Contains(query.Address, System.StringComparison.OrdinalIgnoreCase));
            }

            if (query.MinPrice.HasValue)
            {
                filters.Add(p => p.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                filters.Add(p => p.Price <= query.MaxPrice.Value);
            }

            if (query.YearBuilt.HasValue)
            {
                filters.Add(p => p.Year == query.YearBuilt.Value);
            }

            return filters.Aggregate(properties, (current, filter) => current.Where(filter));
        }

        public IEnumerable<Property> Apply(IEnumerable<Property> properties, ListPropertyWithFiltersQuery query)
        {
            var filters = new List<Func<Property, bool>>();

            if (query.MinPrice.HasValue)
            {
                filters.Add(p => p.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                filters.Add(p => p.Price <= query.MaxPrice.Value);
            }

            return filters.Aggregate(properties, (current, filter) => current.Where(filter));
        }
    }
}
