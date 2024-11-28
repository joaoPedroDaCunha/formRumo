using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rumo.Data;
using Rumo.Data.Repository.AetRepository;
using Rumo.Models;

namespace Rumo.Controllers
{
    public class AetController(Context context,IAetRepository aetRepository) : Controller
    {
        private readonly Context _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var context = await aetRepository.GetAllAsync();
            return View(context);
        }

        [HttpGet]
        [Route("Aet/Index/{id}")]
        public async Task<IActionResult> Index(String id)
        {
            var aet = await aetRepository.GetAllOrderBy(id);
            return View(aet);
        }


        [HttpGet("Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aet = await aetRepository.GetById((Guid)id);
            var versers = await aetRepository.GetVersersByAet((Guid)id);
            ViewData["Versers"] = versers;
            return View(aet);
        }

        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles.OrderBy(a => a.Plate).Where(a => a.Type.Contains("TRATOR")), "Plate", "Plate");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Date,VehicleId,Sigla")] Aet aet)
        {
            await aetRepository.AddAet(aet);    
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aet = await aetRepository.GetById((Guid)id);
            var versers = aetRepository.GetVersersByAet((Guid)id);
            if(aet == null){
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Plate", "Plate", aet.VehicleId);
            ViewData["Versers"] = versers;
            return View(aet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Date,VehicleId,Sigla")] Aet aet)
        {
                try
                {
                   
                    await aetRepository.Update(aet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!aetRepository.AetExists(aet.id))
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aet = await aetRepository.GetById((Guid)id);
            if (aet == null)
            {
                return NotFound();
            }

            return View(aet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
              if (id == null)
            {
                return NotFound();
            }

            await aetRepository.Delete(await aetRepository.GetById((Guid)id));

            return RedirectToAction(nameof(Index));
        }

    }
}
