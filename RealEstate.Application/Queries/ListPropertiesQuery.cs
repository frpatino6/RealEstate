using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class ListPropertiesQuery : IRequest<IEnumerable<Property>>
    {
        public string Address { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? YearBuilt { get; set; }
    }
}
