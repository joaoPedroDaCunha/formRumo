using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rumo.Data;
using Rumo.Models;

namespace Rumo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly Context _context;

    public HomeController(ILogger<HomeController> logger,Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var vehicle = await _context.Vehicles.CountAsync();
        var vehicleUpDate = await _context.Vehicles.Where(a => a.Situation.Contains("Ativo") && a.DuoDate.ToDateTime(TimeOnly.MinValue) >= DateTime.Now).CountAsync();
        var vehicleDownDate = await _context.Vehicles.Where(a => a.Situation.Contains("Ativo") && a.DuoDate.ToDateTime(TimeOnly.MinValue) < DateTime.Now).CountAsync();

        var aet = await _context.Aets.CountAsync();
        var aetUpDate = await _context.Aets.Where(a => a.Date.ToDateTime(TimeOnly.MinValue) >= DateTime.Now).CountAsync();;
        var aetDownDate = await _context.Aets.Where(a => a.Date.ToDateTime(TimeOnly.MinValue) < DateTime.Now).CountAsync();

        var model = new 
        {
            VehicleCount = vehicle,
            VehicleUpDate = vehicleUpDate,
            VehicleDownDate = vehicleDownDate,
            AetCount = aet,
            AetUpDate = aetUpDate,
            AetDownDate = aetDownDate
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
