using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

public static class CreatePlaceOfInterestExtensions
{
    public static async Task<Domain.PlaceOfInterest> ToDbEntity(this CreatePlaceOfInterestRequest request,
        IRepository<StartLocation> startRepository, IRepository<EndLocation> endRepository)
    {
        var dbEntity = new Domain.PlaceOfInterest
        {
            Name = request.Name,
            Description = request.Description,
            PublicUniqueToken = request.PublicUniqueToken,
            ImageBytes = request.ImageBytes,
            ImageType = request.ImageType,
            ImageUrl = request.ImageUrl,
            TerrainScore = request.TerrainScore,
            StartLocation = new StartLocation(),
            EndLocation = new EndLocation()
        };

        if (request.StartLocationId != Guid.Empty)
            dbEntity.StartLocation = await request.GetStartLocationEntity(startRepository);
        else
            dbEntity.StartLocation.Location = request.StartLocation;

        if (request.EndLocationId != Guid.Empty)
            dbEntity.EndLocation = await request.GetEndLocationEntity(endRepository);
        else
            dbEntity.EndLocation.Location = request.EndLocation;

        return dbEntity;
    }

    public static void PreProcess(this CreatePlaceOfInterestRequest request,
        IRepository<Domain.PlaceOfInterest> repository)
    {
        var searchResult = repository.GetQuery().Count(x => x.PublicUniqueToken.Equals(request.PublicUniqueToken));

        if (searchResult > 0) request.PublicUniqueToken = Guid.NewGuid().ToString();
    }

    public static string GeneratePublicToken(this CreatePlaceOfInterestRequest request)
    {
        var rnd = new Random();
        var tokenChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        var token = new string(rnd.GetItems(tokenChars, 8));

        return token;
    }

    private static async Task<StartLocation> GetStartLocationEntity(this CreatePlaceOfInterestRequest request,
        IRepository<StartLocation> repository)
    {
        var startLocation = await repository.GetById(request.StartLocationId);
        return startLocation ?? throw new InvalidOperationException("Start location not found.");
    }

    private static async Task<EndLocation> GetEndLocationEntity(this CreatePlaceOfInterestRequest request,
        IRepository<EndLocation> repository)
    {
        var endLocation = await repository.GetById(request.EndLocationId);
        return endLocation ?? throw new InvalidOperationException("End location not found.");
    }
}