using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.ClientAPI.Dtos;
using PlaceOfInterest.ClientAPI.Extensions;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.ClientAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StartLocationController(IMediator mediator, IRepository<StartLocation> repository) : ControllerBase
{
    [HttpGet]
    public IEnumerable<StartLocationApiDto> Get([FromBody] StartLocationApiRequest request)
    {
        var startLocations = repository.GetAll<StartLocation, string>(request.Skip, request.Take, x => x.Name, true);
        return startLocations.ToApiDto();
    }

    // GET api/<StartLocationController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<StartLocationController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<StartLocationController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<StartLocationController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}