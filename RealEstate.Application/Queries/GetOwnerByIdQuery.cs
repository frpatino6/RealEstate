using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class GetOwnerByIdQuery : IRequest<OwnerDto>
    {
        public Guid IdOwner { get; set; }
    }
}