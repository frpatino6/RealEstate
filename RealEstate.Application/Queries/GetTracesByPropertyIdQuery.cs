using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class GetTracesByPropertyIdQuery : IRequest<IEnumerable<PropertyTrace>>
    {
        public Guid IdProperty { get; set; }
    }
}