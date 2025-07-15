using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain;

namespace PlaceOfInterest.Application.UseCases.DeleteEndLocation;

public static class DeleteEndLocationExtension
{
    public static void PreProcess(this DeleteEndLocationRequest request, IRepository<EndLocation> repository)
    {
        request.Count = repository.GetQuery()
            .Where(x => x.Id == request.Id)
            .Select(x => x.PlaceOfInterests.Select(poi => poi).Count())
            .First();
    }
}