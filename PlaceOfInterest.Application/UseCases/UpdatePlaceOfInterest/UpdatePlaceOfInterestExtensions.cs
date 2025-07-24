using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;

public static class UpdatePlaceOfInterestExtensions
{
    public static UpdatePlaceOfInterestRequest ToUpdateRequest(this Domain.PlaceOfInterest entity)
    {
        return new UpdatePlaceOfInterestRequest
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            PublicUniqueToken = entity.PublicUniqueToken,
            ImageBytes = entity.ImageBytes,
            ImageType = entity.ImageType,
            ImageUrl = entity.ImageUrl,
            TerrainScore = entity.TerrainScore,
            StartLocationId = entity.StartLocation.Id,
            StartLocation = entity.StartLocation.Name,
            EndLocationId = entity.EndLocation.Id,
            EndLocation = entity.EndLocation.Name
        };
    }

    public static async Task UpdateEntity(this Domain.PlaceOfInterest entity, UpdatePlaceOfInterestRequest request,
        IRepository<StartLocation> startLocationRepository, IRepository<EndLocation> endLocationRepository)
    {
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.PublicUniqueToken = request.PublicUniqueToken;
        entity.ImageBytes = request.ImageBytes;
        entity.ImageType = request.ImageType;
        entity.ImageUrl = request.ImageUrl;
        entity.TerrainScore = request.TerrainScore;

        if (entity.StartLocation.Id != request.StartLocationId)
            if (request.StartLocationId != Guid.Empty)
                entity.StartLocation = await request.GetStartLocationEntity(startLocationRepository);

        if (request.StartLocationId == Guid.Empty)
            entity.StartLocation = new StartLocation { Location = request.StartLocation };

        if (entity.EndLocation.Id != request.EndLocationId)
            if (request.EndLocationId != Guid.Empty)
                entity.EndLocation = await request.GetEndLocationEntity(endLocationRepository);

        if (request.EndLocationId == Guid.Empty)
            entity.EndLocation = new EndLocation { Location = request.EndLocation };
    }

    private static async Task<StartLocation> GetStartLocationEntity(this UpdatePlaceOfInterestRequest request,
        IRepository<StartLocation> repository)
    {
        var startLocation = await repository.GetByIdAsync(request.StartLocationId);
        return startLocation ?? throw new InvalidOperationException("Start location not found.");
    }

    private static async Task<EndLocation> GetEndLocationEntity(this UpdatePlaceOfInterestRequest request,
        IRepository<EndLocation> repository)
    {
        var endLocation = await repository.GetByIdAsync(request.EndLocationId);
        return endLocation ?? throw new InvalidOperationException("End location not found.");
    }
}