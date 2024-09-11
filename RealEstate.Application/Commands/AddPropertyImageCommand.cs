using MediatR;

namespace RealEstate.Application.Commands
{
    public class AddPropertyImageCommand : IRequest<Guid>
    {
        public Guid IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
        public string Location { get; set; }
    }
}