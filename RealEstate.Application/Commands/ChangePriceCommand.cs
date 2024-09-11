using MediatR;

namespace RealEstate.Application.Commands
{
    public class ChangePriceCommand : IRequest
    {
        public Guid IdProperty { get; set; }
        public decimal NewPrice { get; set; }
    }
}