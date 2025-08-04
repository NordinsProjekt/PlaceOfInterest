namespace PlaceOfInterest.ClientAPI.Dtos;

public sealed record StartLocationApiDto(Guid Id, string Name, string Description, string Location, bool Verified);

public sealed record EndLocationApiDto(Guid Id, string Name, string Description, string Location, bool Verified);

public sealed record PlaceOfInterestApiDto(
    Guid Id,
    string Name,
    string Description,
    StartLocationApiDto StartLocation,
    EndLocationApiDto EndLocation,
    string ImageUrl,
    byte TerrainScore,
    byte Score,
    bool Verified);

public sealed record StartLocationApiRequest(int Skip, int Take);