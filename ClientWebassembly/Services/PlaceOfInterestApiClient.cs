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
        return await _httpClient.GetFromJsonAsync<List<PlaceOfInterestApiDto>>("api/PlaceOfInterest") ?? new();
    }

    public async Task<PlaceOfInterestApiDto?> GetByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<PlaceOfInterestApiDto>($"api/PlaceOfInterest/{id}");
    }

    public async Task<CreatePlaceOfInterestResponse> CreateAsync(CreatePlaceOfInterestApiRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/PlaceOfInterest", request);

        if (!response.IsSuccessStatusCode) return new(false, "");

        var token = await response.Content.ReadFromJsonAsync<string>();
        if (token is not null) return new(true, token);

        return new(false, "");
    }
}