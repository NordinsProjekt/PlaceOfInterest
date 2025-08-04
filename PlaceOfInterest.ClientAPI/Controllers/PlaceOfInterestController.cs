using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.DeletePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;
using PlaceOfInterest.ClientAPI.Dtos;
using PlaceOfInterest.ClientAPI.Extensions;

namespace PlaceOfInterest.ClientAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaceOfInterestController(
    IMediator mediator,
    IRepository<Domain.PlaceOfInterest> repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PlaceOfInterestApiDto>> Get([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        try
        {
            var pois = repository.GetAll<Domain.PlaceOfInterest, string>(skip, take, x => x.Name, true,
                x => x.StartLocation, x => x.EndLocation);
            return Ok(pois.ToApiDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlaceOfInterestApiDto>> Get(Guid id)
    {
        try
        {
            var poi = await repository.GetByIdAsync(id, x => x.StartLocation, x => x.EndLocation);
            if (poi == null)
                return NotFound();
            return Ok(poi.ToApiDto());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] CreatePlaceOfInterestRequest request)
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
    public async Task<ActionResult<bool>> Put([FromBody] UpdatePlaceOfInterestRequest request)
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
            var result = await mediator.Send(new DeletePlaceOfInterestRequest { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}