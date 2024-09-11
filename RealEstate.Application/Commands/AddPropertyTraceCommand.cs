using MediatR;

namespace RealEstate.Application.Commands
{
    public class AddPropertyTraceCommand : IRequest<Guid>
    {
        public Guid IdProperty { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}