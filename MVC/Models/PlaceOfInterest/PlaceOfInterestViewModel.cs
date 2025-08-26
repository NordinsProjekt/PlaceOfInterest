using Contracts.Models.Dto;

namespace MVC.Models.PlaceOfInterest;

public class PlaceOfInterestViewModel
{
    public List<PlaceOfInterestApiDto> PlaceOfInterests { get; set; } = new();
}
