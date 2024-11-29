using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rumo.Data;
using Rumo.Data.Repository.VehicleRepository;
using Rumo.Models;

namespace Rumo.Controllers
{
    public class VehicleController(IVechicleRepository vechicleRepository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicle = await vechicleRepository.GetAllAsync();
            return View(vehicle);
        }

        [HttpGet]
        [Route("Vehicle/Index/{month}")]
        public async Task<IActionResult> Index(string month)
        {
            var vehicle = await vechicleRepository.Index(month);
            return View(vehicle);
        }

        [HttpGet]
        [Route("Vehicle/Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var vehicle = await vechicleRepository.GetById(id);
            var aet = await vechicleRepository.GetAetByVehicle(id);
            ViewData["Aet"] = aet;
            return View(vehicle);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Plate,Renavam,Chassis,Mark,Version,Type,DuoDate,Situation")] Vehicle vehicle)
        {   
            if (!string.IsNullOrEmpty(vehicle.Plate) && !string.IsNullOrEmpty(vehicle.Chassis) && !string.IsNullOrEmpty(vehicle.Mark) && !string.IsNullOrEmpty(vehicle.Version)){
                if(vehicle.DuoDate.Year > 1990){
                    if(!vechicleRepository.VehicleExists(vehicle)){
                        await vechicleRepository.Create(vehicle);
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(vehicle);

        }

        public async Task<IActionResult> Edit(string id)
        {
            var vehicle = await vechicleRepository.GetById(id);
            return View(vehicle);
        }

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
                await vechicleRepository.Edit(vehicle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vechicleRepository.VehicleExists(vehicle))
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

        public async Task<IActionResult> Delete(string id)
        {
            var vehicle = await vechicleRepository.GetById(id);
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            await vechicleRepository.Delete(await vechicleRepository.GetById(id));
            return RedirectToAction(nameof(Index));
        }

    }
}
