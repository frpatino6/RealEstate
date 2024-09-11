using MediatR;

namespace RealEstate.Application.Commands
{
    public class UpdateOwnerCommand : IRequest
    {
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
