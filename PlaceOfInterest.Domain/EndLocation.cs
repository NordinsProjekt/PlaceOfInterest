namespace PlaceOfInterest.Domain;

public class EndLocation
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public bool Verified { get; set; }
    public List<PlaceOfInterest> PlaceOfInterests { get; set; } = new();
}