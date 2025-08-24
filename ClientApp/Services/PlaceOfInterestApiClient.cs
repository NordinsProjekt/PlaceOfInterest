using System.Net.Http.Json;

namespace ClientApp.Services;

public class PlaceOfInterestApiClient
{
    private readonly HttpClient _httpClient;
    private const string BaseRoute = "api/placeofinterest";

    public PlaceOfInterestApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PlaceOfInterestApiDto>> GetAllAsync(int skip = 0, int take = 20)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<PlaceOfInterestApiDto>>($"{BaseRoute}?skip={skip}&take={take}");
            return response ?? Array.Empty<PlaceOfInterestApiDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching places of interest: {ex.Message}");
            return Array.Empty<PlaceOfInterestApiDto>();
        }
    }

    public async Task<bool> CreateAsync(CreatePlaceOfInterestRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseRoute, request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating place of interest: {ex.Message}");
            return false;
        }
    }
}