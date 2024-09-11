using MediatR;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.Queries
{
    public class GetTracesByPropertyIdQueryHandler : IRequestHandler<GetTracesByPropertyIdQuery, IEnumerable<PropertyTrace>>
    {
        private readonly IPropertyTraceRepository _propertyTraceRepository;

        public GetTracesByPropertyIdQueryHandler(IPropertyTraceRepository propertyTraceRepository)
        {
            _propertyTraceRepository = propertyTraceRepository;
        }

        public async Task<IEnumerable<PropertyTrace>> Handle(GetTracesByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            return await _propertyTraceRepository.GetByPropertyIdAsync(request.IdProperty);
        }
    }
}