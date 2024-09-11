using MediatR;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class GetOwnerByIdQuery : IRequest<Owner>
    {
        public Guid IdOwner { get; set; }
    }
}