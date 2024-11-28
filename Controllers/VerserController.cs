using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Rumo.Data;
using Rumo.Models;

namespace Rumo.Controllers
{
    public class VerserController : Controller
    {
        private readonly Context _context;

        public VerserController(Context context)
        {
            _context = context;
        }

        // GET: Verser
        public async Task<IActionResult> Index()
        {
            var context = _context.Versers.Include(v => v.Aet).Include(v => v.Vehicle);
            return View(await context.ToListAsync());
        }

        // GET: Verser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verser = await _context.Versers
                .Include(v => v.Aet)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.id == id);
            if (verser == null)
            {
                return NotFound();
            }

            return View(verser);
        }

        // GET: Verser/Create

        [HttpGet("create/{id:Guid}")]
        public IActionResult Create(Guid? id)
        {
             if (id == null)
            {
                return NotFound();
            }

            Verser verser = new()
            {
                AetId = (Guid)id
            };
            ViewData["AetId"] = new SelectList(_context.Aets.OrderBy(a => a.id), "id", "id");
            ViewData["VehicleId"] = new SelectList(_context.Vehicles.OrderBy(v => v.Plate).Where(a => a.Type.Contains("REBOQUE")), "Plate", "Plate");
            return View(verser);
        }

        // POST: Verser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,AetId,VehicleId")] Verser verser)
        {
                verser.id = Guid.NewGuid();
                _context.Add(verser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","Aet",new {id = verser.AetId});  
        }

        // GET: Verser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verser = await _context.Versers.FindAsync(id);
            if (verser == null)
            {
                return NotFound();
            }
            ViewData["AetId"] = new SelectList(_context.Aets, "id", "id", verser.AetId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Plate", "Plate", verser.VehicleId);
            return View(verser);
        }

        // POST: Verser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,AetId,VehicleId")] Verser verser)
        {
            if (id != verser.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerserExists(verser.id))
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
            ViewData["AetId"] = new SelectList(_context.Aets, "id", "id", verser.AetId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Plate", "Plate", verser.VehicleId);
            return View(verser);
        }

        // GET: Verser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verser = await _context.Versers
                .Include(v => v.Aet)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(m => m.id == id);
            if (verser == null)
            {
                return NotFound();
            }

            return View(verser);
        }

        // POST: Verser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var verser = await _context.Versers.FindAsync(id);
             if (verser == null)
            {
                return NotFound();
            }
            var aetid = verser.AetId;
            if (verser != null)
            {
                _context.Versers.Remove(verser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Aet",new {id = aetid});
        }

        private bool VerserExists(Guid id)
        {
            return _context.Versers.Any(e => e.id == id);
        }
    }
}
