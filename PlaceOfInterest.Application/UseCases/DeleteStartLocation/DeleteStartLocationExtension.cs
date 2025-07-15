using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.DeleteStartLocation;

public static class DeleteStartLocationExtension
{
    public static void PreProcess(this DeleteStartLocationRequest request, IRepository<StartLocation> repository)
    {
        request.Count = repository.GetQuery()
            .Where(x => x.Id == request.Id)
            .Select(x => x.PlaceOfInterests.Select(poi => poi).Count())
            .First();
    }
}