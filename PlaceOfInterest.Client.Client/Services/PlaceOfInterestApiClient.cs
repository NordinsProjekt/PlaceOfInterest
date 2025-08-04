using System.Net.Http.Json;
using PlaceOfInterest.ClientAPI.Dtos;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;

namespace PlaceOfInterest.Client.Client.Services;

public class PlaceOfInterestApiClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/placeofinterest";

    public PlaceOfInterestApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PlaceOfInterestApiDto>> GetAllAsync(int skip = 0, int take = 20)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PlaceOfInterestApiDto>>($"{BaseUrl}?skip={skip}&take={take}") 
               ?? Array.Empty<PlaceOfInterestApiDto>();
    }

    public async Task<PlaceOfInterestApiDto?> GetByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<PlaceOfInterestApiDto>($"{BaseUrl}/{id}");
    }

    public async Task<bool> CreateAsync(CreatePlaceOfInterestRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> UpdateAsync(UpdatePlaceOfInterestRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{request.Id}", request);
        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}