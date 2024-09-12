using MediatR;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Queries
{
    public class GetPropertyByIdQuery : IRequest<PropertyDto>
    {
        public Guid Id { get; set; }
    }
}
