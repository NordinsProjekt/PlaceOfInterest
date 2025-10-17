using System.Net.Http.Json;
using Contracts.Models.Dto;
using Contracts.Models.Requests;
using Contracts.Models.Responses;

namespace ClientWebassembly.Services;

public class PlaceOfInterestApiClient
{
    private readonly HttpClient _httpClient;

    public PlaceOfInterestApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PlaceOfInterestApiDto>> GetAllAsync()
    {
        try
        {
            Console.WriteLine($"Making request to: {_httpClient.BaseAddress}api/PlaceOfInterest");
            var response = await _httpClient.GetAsync("api/PlaceOfInterest");
            
            Console.WriteLine($"Response status: {response.StatusCode}");
            Console.WriteLine($"Response headers: {string.Join(", ", response.Headers.Select(h => $"{h.Key}:{string.Join(",", h.Value)}"))}");
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<PlaceOfInterestApiDto>>();
                return result ?? new();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response content: {content}");
                return new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GetAllAsync: {ex.Message}");
            Console.WriteLine($"Exception type: {ex.GetType().Name}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<PlaceOfInterestApiDto?> GetByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<PlaceOfInterestApiDto>($"api/PlaceOfInterest/{id}");
    }

    public async Task<CreatePlaceOfInterestResponse> CreateAsync(CreatePlaceOfInterestApiRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/PlaceOfInterest", request);

        if (!response.IsSuccessStatusCode) return new(false, "");

        var content = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrWhiteSpace(content)) return new(true, content.Trim('"'));

        return new(false, "");
    }
}