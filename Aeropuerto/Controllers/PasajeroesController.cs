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
    public class PasajeroesController : Controller
    {
        private readonly AeropuertoContext _context;

        public PasajeroesController(AeropuertoContext context)
        {
            _context = context;
        }

        // GET: Pasajeroes
        public async Task<IActionResult> Index()
        {
            var aeropuertoContext = _context.Pasajeros.Include(p => p.Destino);
            return View(await aeropuertoContext.ToListAsync());
        }

        // GET: Pasajeroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pasajeros == null)
            {
                return NotFound();
            }

            var pasajero = await _context.Pasajeros
                .Include(p => p.Destino)
                .FirstOrDefaultAsync(m => m.PasajeroId == id);
            if (pasajero == null)
            {
                return NotFound();
            }

            return View(pasajero);
        }

        // GET: Pasajeroes/Create
        public IActionResult Create()
        {
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId");
            return View();
        }

        // POST: Pasajeroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PasajeroId,Nombre,Apellidos,Edad,DestinoId")] Pasajero pasajero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasajero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", pasajero.DestinoId);
            return View(pasajero);
        }

        // GET: Pasajeroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pasajeros == null)
            {
                return NotFound();
            }

            var pasajero = await _context.Pasajeros.FindAsync(id);
            if (pasajero == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", pasajero.DestinoId);
            return View(pasajero);
        }

        // POST: Pasajeroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PasajeroId,Nombre,Apellidos,Edad,DestinoId")] Pasajero pasajero)
        {
            if (id != pasajero.PasajeroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasajero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasajeroExists(pasajero.PasajeroId))
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
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", pasajero.DestinoId);
            return View(pasajero);
        }

        // GET: Pasajeroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pasajeros == null)
            {
                return NotFound();
            }

            var pasajero = await _context.Pasajeros
                .Include(p => p.Destino)
                .FirstOrDefaultAsync(m => m.PasajeroId == id);
            if (pasajero == null)
            {
                return NotFound();
            }

            return View(pasajero);
        }

        // POST: Pasajeroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pasajeros == null)
            {
                return Problem("Entity set 'AeropuertoContext.Pasajeros'  is null.");
            }
            var pasajero = await _context.Pasajeros.FindAsync(id);
            if (pasajero != null)
            {
                _context.Pasajeros.Remove(pasajero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasajeroExists(int id)
        {
          return (_context.Pasajeros?.Any(e => e.PasajeroId == id)).GetValueOrDefault();
        }
    }
}
