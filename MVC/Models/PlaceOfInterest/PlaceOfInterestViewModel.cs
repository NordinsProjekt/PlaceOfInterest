using PlaceOfInterest.ClientAPI.Dtos;

namespace MVC.Models.PlaceOfInterest;

public class PlaceOfInterestViewModel
{
    public List<PlaceOfInterestApiDto> PlaceOfInterests { get; set; } = new();
}
