using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries
{
    public class ListPropertyWithFiltersQuery : IRequest<IEnumerable<PropertyDto>>
    {
        public string Location { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MaxBedrooms { get; set; }
        public int? MinBathrooms { get; set; }
        public int? MaxBathrooms { get; set; }
    }
}