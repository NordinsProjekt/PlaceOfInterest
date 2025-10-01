using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PlaceOfInterest.GoogleMaps;

public class GoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GoogleMapsService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<LocationResult?> GetLocationFromCoordinatesAsync(double latitude, double longitude)
    {
        var response = await _httpClient.GetFromJsonAsync<GeocodeResponse>(
            $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={_apiKey}");

        if (response?.Results == null || response.Results.Length == 0)
            return null;

        var result = response.Results[0];
        return new LocationResult
        {
            FormattedAddress = result.FormattedAddress,
            Latitude = result.Geometry.Location.Lat,
            Longitude = result.Geometry.Location.Lng,
            City = result.AddressComponents.FirstOrDefault(x => x.Types.Contains("locality"))?.LongName ?? "",
            Country = result.AddressComponents.FirstOrDefault(x => x.Types.Contains("country"))?.LongName ?? ""
        };
    }

    public async Task<LocationResult?> GetLocationFromAddressAsync(string address)
    {
        var response = await _httpClient.GetFromJsonAsync<GeocodeResponse>(
            $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}");

        if (response?.Results == null || response.Results.Length == 0)
            return null;

        var result = response.Results[0];
        return new LocationResult
        {
            FormattedAddress = result.FormattedAddress,
            Latitude = result.Geometry.Location.Lat,
            Longitude = result.Geometry.Location.Lng,
            City = result.AddressComponents.FirstOrDefault(x => x.Types.Contains("locality"))?.LongName ?? "",
            Country = result.AddressComponents.FirstOrDefault(x => x.Types.Contains("country"))?.LongName ?? ""
        };
    }
}

public class LocationResult
{
    public string FormattedAddress { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
}

// Google Maps API Response Models
internal class GeocodeResponse
{
    [JsonPropertyName("results")]
    public GeocodeResult[] Results { get; set; } = [];

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";
}

internal class GeocodeResult
{
    [JsonPropertyName("address_components")]
    public AddressComponent[] AddressComponents { get; set; } = [];

    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; } = "";

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; } = new();
}

internal class AddressComponent
{
    [JsonPropertyName("long_name")]
    public string LongName { get; set; } = "";

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = "";

    [JsonPropertyName("types")]
    public string[] Types { get; set; } = [];
}

internal class Geometry
{
    [JsonPropertyName("location")]
    public Location Location { get; set; } = new();
}

internal class Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}