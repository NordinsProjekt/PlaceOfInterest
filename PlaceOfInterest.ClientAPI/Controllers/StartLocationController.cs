using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.UseCases.CreateStartLocation;
using PlaceOfInterest.Application.UseCases.DeleteStartLocation;
using PlaceOfInterest.Application.UseCases.UpdateStartLocation;
using PlaceOfInterest.ClientAPI.Dtos;
using PlaceOfInterest.ClientAPI.Extensions;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.ClientAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartLocationController(IMediator mediator, IRepository<StartLocation> repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<StartLocationApiDto>> Get([FromQuery] StartLocationApiRequest request)
    {
        try
        {
            var startLocations = repository.GetAll<StartLocation, string>(
                request.Skip,
                request.Take,
                x => x.Name,
                true);

            return Ok(startLocations.ToApiDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StartLocationApiDto>> Get(Guid id)
    {
        try
        {
            var startLocation = await repository.GetById(id);
            if (startLocation == null)
                return NotFound();

            return Ok(startLocation.ToApiDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] CreateStartLocationRequest request)
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
    public async Task<ActionResult<bool>> Put([FromBody] UpdateStartLocationRequest request)
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
            var result = await mediator.Send(new DeleteStartLocationRequest { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}