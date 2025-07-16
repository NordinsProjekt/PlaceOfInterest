using PlaceOfInterest.Domain.Interface;

namespace PlaceOfInterest.Application.Models;

public class DropDownNameIdDto : INameId
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
}