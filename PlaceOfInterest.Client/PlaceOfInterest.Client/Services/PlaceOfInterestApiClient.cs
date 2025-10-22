using System.Net.Http.Json;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.Application.UseCases.UpdatePlaceOfInterest;
using PlaceOfInterest.ClientAPI.Dtos;

namespace PlaceOfInterest.Client.Services;

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
            var response =
                await _httpClient.GetFromJsonAsync<IEnumerable<PlaceOfInterestApiDto>>(
                    $"{BaseRoute}?skip={skip}&take={take}");
            return response ?? Array.Empty<PlaceOfInterestApiDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching places of interest: {ex.Message}");
            return Array.Empty<PlaceOfInterestApiDto>();
        }
    }

    public async Task<PlaceOfInterestApiDto?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PlaceOfInterestApiDto>($"{BaseRoute}/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching place of interest: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateAsync(CreatePlaceOfInterestRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseRoute, request);
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<bool>();

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error creating place of interest: {error}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating place of interest: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePlaceOfInterestRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseRoute}/{id}", request);
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<bool>();

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error updating place of interest: {error}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating place of interest: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<bool>();

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error deleting place of interest: {error}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting place of interest: {ex.Message}");
            return false;
        }
    }
}
