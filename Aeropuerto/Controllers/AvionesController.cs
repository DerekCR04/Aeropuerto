using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aeropuerto.Models;

namespace Aeropuerto.Controllers
{
    public class AvionesController : Controller
    {
        private readonly AeropuertoContext _context;

        public AvionesController(AeropuertoContext context)
        {
            _context = context;
        }

        // GET: Aviones
        public async Task<IActionResult> Index()
        {
            var aeropuertoContext = _context.Aviones.Include(a => a.Aerolinea);
            return View(await aeropuertoContext.ToListAsync());
        }

        // GET: Aviones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aviones == null)
            {
                return NotFound();
            }

            var avione = await _context.Aviones
                .Include(a => a.Aerolinea)
                .FirstOrDefaultAsync(m => m.AvionId == id);
            if (avione == null)
            {
                return NotFound();
            }

            return View(avione);
        }

        public JsonResult GetAviones()
        {
            var aviones = _context.Aviones.Select(a => new { a.AvionId, a.Modelo }).ToList();
            return Json(aviones);
        }

        // GET: Aviones/Create
        public IActionResult Create()
        {
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId");
            return View();
        }

        // POST: Aviones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvionId,Modelo,Capacidad,AerolineaId")] Avione avione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", avione.AerolineaId);
            return View(avione);
        }

        // GET: Aviones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aviones == null)
            {
                return NotFound();
            }

            var avione = await _context.Aviones.FindAsync(id);
            if (avione == null)
            {
                return NotFound();
            }
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", avione.AerolineaId);
            return View(avione);
        }

        // POST: Aviones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvionId,Modelo,Capacidad,AerolineaId")] Avione avione)
        {
            if (id != avione.AvionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvioneExists(avione.AvionId))
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
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", avione.AerolineaId);
            return View(avione);
        }

        // GET: Aviones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aviones == null)
            {
                return NotFound();
            }

            var avione = await _context.Aviones
                .Include(a => a.Aerolinea)
                .FirstOrDefaultAsync(m => m.AvionId == id);
            if (avione == null)
            {
                return NotFound();
            }

            return View(avione);
        }

        // POST: Aviones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aviones == null)
            {
                return Problem("Entity set 'AeropuertoContext.Aviones'  is null.");
            }
            var avione = await _context.Aviones.FindAsync(id);
            if (avione != null)
            {
                _context.Aviones.Remove(avione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvioneExists(int id)
        {
          return (_context.Aviones?.Any(e => e.AvionId == id)).GetValueOrDefault();
        }
    }
}
