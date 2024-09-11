using MediatR;

namespace RealEstate.Application.Commands
{
    public class CreatePropertyCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public string Address { get; set; }
        public Guid IdOwner { get; set; }
    }
}