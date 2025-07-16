using PlaceOfInterest.Application.Interfaces;

namespace PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

public static class CreatePlaceOfInterestExtensions
{
    public static Domain.PlaceOfInterest ToDbEntity(this CreatePlaceOfInterestRequest request)
    {
        return new Domain.PlaceOfInterest
        {
            Name = request.Name,
            Description = request.Description,
            PublicUniqueToken = request.PublicUniqueToken,
            ImageBytes = request.ImageBytes,
            ImageType = request.ImageType,
            ImageUrl = request.ImageUrl,
            TerrainScore = request.TerrainScore,
            StartLocation = request.StartLocation,
            EndLocation = request.EndLocation
        };
    }

    public static void PreProcess(this CreatePlaceOfInterestRequest request,
        IRepository<Domain.PlaceOfInterest> repository)
    {
        var rnd = new Random();
        var tokenChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        var token = rnd.GetItems(tokenChars, 6).ToString();

        var searchResult = repository.GetQuery().Count(x => x.PublicUniqueToken.Equals(token));

        request.PublicUniqueToken = searchResult! > 0 ? Guid.NewGuid().ToString() : token!;
    }
}