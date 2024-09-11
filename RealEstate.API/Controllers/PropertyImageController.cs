using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands;
using RealEstate.Application.Queries;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] AddPropertyImageCommand command)
        {
            var imageId = await _mediator.Send(command);
            return Ok(imageId);
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetImagesByPropertyId(Guid propertyId)
        {
            var query = new GetImagesByPropertyIdQuery { IdProperty = propertyId };
            var images = await _mediator.Send(query);
            return Ok(images);
        }


    }
}
