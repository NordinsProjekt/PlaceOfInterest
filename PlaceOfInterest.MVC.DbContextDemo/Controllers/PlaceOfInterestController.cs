using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.EFCore;

namespace PlaceOfInterest.MVC.DbContextDemo.Controllers;

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
    public async Task<IActionResult> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("name", "Name is required");
            return View();
        }
        db.PlaceOfInterests.Add(new PlaceOfInterest.Domain.PlaceOfInterest
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            StartLocation = new PlaceOfInterest.Domain.StartLocation { Id = Guid.NewGuid(), Name = "Temp Start" },
            EndLocation = new PlaceOfInterest.Domain.EndLocation { Id = Guid.NewGuid(), Name = "Temp End" }
        });
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}