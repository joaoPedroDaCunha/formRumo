using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rumo.Data;
using Rumo.Models;

namespace Rumo.Controllers
{
    public class AetController : Controller
    {
        private readonly Context _context;

        public AetController(Context context)
        {
            _context = context;
        }

        // GET: Aet
        public async Task<IActionResult> Index()
        {
            var context = _context.Aets.Include(a => a.Vehicle).Include(a =>a.Versers);
            return View(await context.ToListAsync());
        }

        // GET: Aet/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aet = await _context.Aets
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(m => m.id == id);
            if (aet == null)
            {
                return NotFound();
            }
            var versers = _context.Versers.Where(a => a.AetId.Equals(id)).Include(a => a.Vehicle).ToList();
            ViewData["Versers"] = versers.ToList();
            return View(aet);
        }

        // GET: Aet/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Plate", "Plate").OrderBy(v => v.Value);
            return View();
        }

        // POST: Aet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Date,VehicleId,Sigla")] Aet aet)
        {
            
                aet.id = Guid.NewGuid();
                _context.Add(aet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Aet/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aet = await _context.Aets.FindAsync(id);
            if (aet == null)
            {
                return NotFound();
            }
            var versers = _context.Versers.Where(a => a.AetId.Equals(id)).ToList();
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Plate", "Plate", aet.VehicleId);
            ViewData["Versers"] = versers.ToList();
            return View(aet);
        }

        // POST: Aet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Date,VehicleId,Sigla")] Aet aet)
        {
            if (id != aet.id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(aet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AetExists(aet.id))
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

        // GET: Aet/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aet = await _context.Aets
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(m => m.id == id);
            if (aet == null)
            {
                return NotFound();
            }

            return View(aet);
        }

        // POST: Aet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aet = await _context.Aets.FindAsync(id);
            if (aet != null)
            {
                _context.Aets.Remove(aet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AetExists(Guid id)
        {
            return _context.Aets.Any(e => e.id == id);
        }
    }
}
