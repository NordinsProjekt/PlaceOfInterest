using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.EFCore;
using PlaceOfInterest.MVC.MediatorDemo;
using Contacts = Contracts.Models.Requests;

namespace PlaceOfInterest.MVC.MediatorDemo.Controllers;

public class PlaceOfInterestController(PlaceOfInterestContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await db.PlaceOfInterests
            .Include(x => x.StartLocation)
            .Include(x => x.EndLocation)
            .OrderBy(x => x.Name)
            .Take(20)
            .ToListAsync();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Contacts.CreatePlaceOfInterestApiRequestDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        
        if (result) return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Failed to create place of interest.");
        return View(model);
    }
}