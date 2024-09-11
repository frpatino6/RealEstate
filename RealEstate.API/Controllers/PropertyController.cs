using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Commands;
using RealEstate.Application.Queries;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropertyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePropertyCommand command)
    {
        var propertyId = await _mediator.Send(command);
        return Ok(propertyId);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListPropertiesQuery query)
    {
        var properties = await _mediator.Send(query);
        return Ok(properties);
    }
}
