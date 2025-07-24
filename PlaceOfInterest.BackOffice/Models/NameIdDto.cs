using PlaceOfInterest.Domain.Interface;

namespace PlaceOfInterest.BackOffice.Models;

public class NameIdDto : INameId
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
}
