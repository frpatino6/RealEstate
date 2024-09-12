using MediatR;
using RealEstate.Application.DTOs;

namespace RealEstate.Application.Queries
{
    public class GetAllOwnersQuery : IRequest<IEnumerable<OwnerDto>>
    {
    }
}
