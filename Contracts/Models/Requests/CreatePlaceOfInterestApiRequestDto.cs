namespace Contracts.ApiDtos.Requests;

public class CreatePlaceOfInterestApiRequestDto(
    string Name,
    string Description,
    string StartLocation,
    string EndLocation,
    string ImageUrl);
