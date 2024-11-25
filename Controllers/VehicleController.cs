using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rumo.Data;
using Rumo.Models;

namespace Rumo.Controllers
{
    public class VehicleController : Controller
    {
        private readonly Context _context;

        public VehicleController(Context context)
        {
            _context = context;
        }

        // GET: Vehicle
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles.OrderBy(v => v.Plate).ToListAsync());
        }

        [HttpGet("Vehicle/Index/{month}")]
        public async Task<IActionResult> Index(string month)
        {
            switch (month){
                case "Julho" :
                    return View(await _context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("1") || v.Plate.EndsWith("2"))).ToListAsync());
                case "Agosto" :
                    return View(await _context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("3") || v.Plate.EndsWith("4"))).ToListAsync());
                case "Setembro" :
                    return View(await _context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("5") || v.Plate.EndsWith("6"))|| v.Type.Contains("TRATOR") &&(v.Plate.EndsWith("1")||v.Plate.EndsWith("2"))).ToListAsync());
                case "Outubro":
                    return View(await _context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && (v.Plate.EndsWith("7") || v.Plate.EndsWith("8"))|| v.Type.Contains("TRATOR") &&(v.Plate.EndsWith("3")||v.Plate.EndsWith("4") || v.Plate.EndsWith("5"))).ToListAsync());
                case "Novembro":
                    return View(await _context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && v.Plate.EndsWith("9") || v.Type.Contains("TRATOR") && (v.Plate.EndsWith("6") || v.Plate.EndsWith("7") || v.Plate.EndsWith("8"))).ToListAsync());
                case "Dezembro":
                    return View(await _context.Vehicles.OrderBy(v => v.Plate).Where(v => (v.Type.Contains("REBOQUE") || v.Type.Contains("AUTOMOVEL")) && v.Plate.EndsWith("0") || v.Type.Contains("TRATOR") && (v.Plate.EndsWith("9") || v.Plate.EndsWith("8"))).ToListAsync());
                default :
                    return RedirectToAction(nameof(Index));
            }
        }

        // GET: Vehicle/Details/5
        [HttpGet]
        [Route("Vehicle/Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Plate.Contains(id) || m.Renavam.Contains(id) || m.Chassis.Contains(id));
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Plate,Renavam,Chassis,Mark,Version,Type,DuoDate,Situation")] Vehicle vehicle)
        {   
            if (!string.IsNullOrEmpty(vehicle.Plate) && !string.IsNullOrEmpty(vehicle.Chassis) && !string.IsNullOrEmpty(vehicle.Mark) && !string.IsNullOrEmpty(vehicle.Version)){
                if(vehicle.DuoDate.Year > 1990){
                    bool existe = await _context.Vehicles.AnyAsync(v => v.Plate.Contains(vehicle.Plate) || v.Chassis.Contains(vehicle.Chassis) || v.Renavam.Contains(vehicle.Renavam));
                    if(!existe){
                        _context.Add(vehicle);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(vehicle);

        }
        
        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Plate,Renavam,Chassis,Mark,Version,Type,DuoDate,Situation")] Vehicle vehicle)
        {
            if (id != vehicle.Plate)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Plate))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Plate == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            var aet = await _context.Aets.Where(a => a.VehicleId.Equals(id)).ToListAsync();
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                if(aet != null){

                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.Plate == id);
        }
    }
}
