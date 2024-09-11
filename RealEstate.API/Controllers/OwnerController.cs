using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands;
using RealEstate.Application.Queries;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerCommand command)
        {
            var ownerId = await _mediator.Send(command);
            return Ok(ownerId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var query = new GetOwnerByIdQuery { IdOwner = id };
            var owner = await _mediator.Send(query);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _mediator.Send(new GetAllOwnersQuery());
            return Ok(owners);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] UpdateOwnerCommand command)
        {
            if (id != command.IdOwner)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
