using System.Net.Http.Json;
using Contracts.ApiDtos.Requests;
using Contracts.Models.Dto;

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
        return await _httpClient.GetFromJsonAsync<List<PlaceOfInterestApiDto>>("api/PlaceOfInterest") ?? new();
    }

    public async Task<PlaceOfInterestApiDto?> GetByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<PlaceOfInterestApiDto>($"api/PlaceOfInterest/{id}");
    }

    public async Task<bool> CreateAsync(CreatePlaceOfInterestApiRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/PlaceOfInterest", request);
        return response.IsSuccessStatusCode;
    }
}