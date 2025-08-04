using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.UseCases.CreateEndLocation;
using PlaceOfInterest.Application.UseCases.DeleteEndLocation;
using PlaceOfInterest.Application.UseCases.UpdateEndLocation;
using PlaceOfInterest.ClientAPI.Dtos;
using PlaceOfInterest.ClientAPI.Extensions;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.ClientAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EndLocationController(IMediator mediator, IRepository<EndLocation> repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<EndLocationApiDto>> Get([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        try
        {
            var endLocations = repository.GetAll<EndLocation, string>(skip, take, x => x.Name, true);
            return Ok(endLocations.ToApiDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EndLocationApiDto>> Get(Guid id)
    {
        try
        {
            var endLocation = await repository.GetByIdAsync(id);
            if (endLocation == null)
                return NotFound();
            return Ok(endLocation.ToApiDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] CreateEndLocationRequest request)
    {
        try
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<bool>> Put([FromBody] UpdateEndLocationRequest request)
    {
        try
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        try
        {
            var result = await mediator.Send(new DeleteEndLocationRequest { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}