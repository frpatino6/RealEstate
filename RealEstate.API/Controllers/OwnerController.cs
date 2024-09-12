using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands;
using RealEstate.Application.DTOs;
using RealEstate.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Propietarios")]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo propietario.
        /// </summary>
        /// <param name="command">Comando con los datos del propietario a crear.</param>
        /// <returns>El ID del propietario creado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear un nuevo propietario", Description = "Este endpoint crea un nuevo propietario.")]
        public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerCommand command)
        {
            var ownerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOwnerById), new { id = ownerId }, ownerId);
        }

        /// <summary>
        /// Obtiene un propietario por ID.
        /// </summary>
        /// <param name="id">El ID del propietario.</param>
        /// <returns>El propietario con el ID especificado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Obtener un propietario por ID", Description = "Devuelve un propietario utilizando su ID.")]
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

        /// <summary>
        /// Lista todos los propietarios.
        /// </summary>
        /// <returns>Lista de propietarios.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OwnerDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar todos los propietarios", Description = "Devuelve una lista de todos los propietarios.")]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _mediator.Send(new GetAllOwnersQuery());
            return Ok(owners);
        }

        /// <summary>
        /// Actualiza los datos de un propietario existente.
        /// </summary>
        /// <param name="id">ID del propietario que se va a actualizar.</param>
        /// <param name="command">Comando con los datos del propietario actualizados.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Actualizar propietario", Description = "Actualiza los datos de un propietario existente.")]
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
