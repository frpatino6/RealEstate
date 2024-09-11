using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands;
using RealEstate.Application.Queries;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyTraceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddTrace([FromBody] AddPropertyTraceCommand command)
        {
            var traceId = await _mediator.Send(command);
            return Ok(traceId);
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetTracesByPropertyId(Guid propertyId)
        {
            var query = new GetTracesByPropertyIdQuery { IdProperty = propertyId };
            var traces = await _mediator.Send(query);
            return Ok(traces);
        }

    }
}
