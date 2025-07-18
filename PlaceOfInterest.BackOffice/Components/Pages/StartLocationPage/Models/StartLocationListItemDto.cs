namespace PlaceOfInterest.BackOffice.Components.Pages.StartLocationPage.Models;

public class StartLocationListItemDto
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public bool Verified { get; set; }
}
