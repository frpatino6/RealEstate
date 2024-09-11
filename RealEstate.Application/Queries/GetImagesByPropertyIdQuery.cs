using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class GetImagesByPropertyIdQuery : IRequest<IEnumerable<PropertyImage>>
    {
        public Guid IdProperty { get; set; }
    }
}