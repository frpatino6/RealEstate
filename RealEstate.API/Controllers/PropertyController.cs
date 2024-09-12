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
    [Tags("Propiedades")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea una nueva propiedad.
        /// </summary>
        /// <param name="command">Comando con los datos de la propiedad a crear.</param>
        /// <returns>El ID de la propiedad creada.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear una nueva propiedad", Description = "Este endpoint crea una nueva propiedad.")]
        public async Task<IActionResult> Create([FromBody] CreatePropertyCommand command)
        {
            var propertyId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = propertyId }, propertyId);
        }

        /// <summary>
        /// Obtiene una propiedad por ID.
        /// </summary>
        /// <param name="id">El ID de la propiedad.</param>
        /// <returns>La propiedad con el ID especificado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Obtener una propiedad por ID", Description = "Devuelve una propiedad utilizando su ID.")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var property = await _mediator.Send(new GetPropertyByIdQuery { Id = id });
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        /// <summary>
        /// Lista las propiedades aplicando filtros.
        /// </summary>
        /// <param name="query">Filtros de búsqueda para las propiedades.</param>
        /// <returns>Lista de propiedades filtradas.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PropertyDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar propiedades", Description = "Obtiene una lista de propiedades filtradas.")]
        public async Task<IActionResult> GetAll([FromQuery] ListPropertyWithFiltersQuery query)
        {
            var properties = await _mediator.Send(query);
            return Ok(properties);
        }
    }
}
