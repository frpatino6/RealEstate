using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Commands
{
    public class AddPropertyTraceCommandHandler : IRequestHandler<AddPropertyTraceCommand, Guid>
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;

        public AddPropertyTraceCommandHandler(IPropertyTraceRepository propertyTraceRepository)
        {
            _propertyTraceRepository = propertyTraceRepository;
        }

        public async Task<Guid> Handle(AddPropertyTraceCommand request, CancellationToken cancellationToken)
        {
            var trace = new PropertyTrace
            {
                IdPropertyTrace = Guid.NewGuid(),
                IdProperty = request.IdProperty,
                DateSale = request.DateSale,
                Name = request.Name,
                Value = request.Value,
                Tax = request.Tax
            };

            await _propertyTraceRepository.AddAsync(trace);

            return trace.IdPropertyTrace;
        }
    }
}