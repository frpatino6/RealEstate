using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class GetAllOwnersQuery : IRequest<IEnumerable<Owner>>
    {
    }
}
