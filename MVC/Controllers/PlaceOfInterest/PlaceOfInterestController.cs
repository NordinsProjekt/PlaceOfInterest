using Contracts.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.PlaceOfInterest;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;

namespace MVC.Controllers.PlaceOfInterest;

public class PlaceOfInterestController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly string _apiBaseUrl = "https://localhost:7049/api/PlaceOfInterest";

    public async Task<IActionResult> Index()
    {
        var client = httpClientFactory.CreateClient();
        var places = await client.GetFromJsonAsync<List<PlaceOfInterestApiDto>>(_apiBaseUrl);
        var model = new PlaceOfInterestViewModel { PlaceOfInterests = places ?? new() };
        return View(model);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var client = httpClientFactory.CreateClient();
        var place = await client.GetFromJsonAsync<PlaceOfInterestApiDto>($"{_apiBaseUrl}/{id}");
        if (place == null)
            return NotFound();
        return View(place);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePlaceOfInterestRequest model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var client = httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync(_apiBaseUrl, model);
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));
        ModelState.AddModelError("", "Failed to create place of interest.");
        return View(model);
    }
}
