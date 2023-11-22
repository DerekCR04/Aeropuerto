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
    public class VueloesController : Controller
    {
        private readonly AeropuertoContext _context;

        public VueloesController(AeropuertoContext context)
        {
            _context = context;
        }

        // GET: Vueloes
        public async Task<IActionResult> Index()
        {
            var aeropuertoContext = _context.Vuelos.Include(v => v.Aerolinea).Include(v => v.Avion).Include(v => v.Destino);
            return View(await aeropuertoContext.ToListAsync());
        }

        // GET: Vueloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos
                .Include(v => v.Aerolinea)
                .Include(v => v.Avion)
                .Include(v => v.Destino)
                .FirstOrDefaultAsync(m => m.VueloId == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // GET: Vueloes/Create
        public IActionResult Create()
        {
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId");
            ViewData["AvionId"] = new SelectList(_context.Aviones, "AvionId", "AvionId");
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId");
            return View();
        }

        // POST: Vueloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VueloId,AvionId,AerolineaId,DestinoId")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", vuelo.AerolineaId);
            ViewData["AvionId"] = new SelectList(_context.Aviones, "AvionId", "AvionId", vuelo.AvionId);
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", vuelo.DestinoId);
            return View(vuelo);
        }

        // GET: Vueloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", vuelo.AerolineaId);
            ViewData["AvionId"] = new SelectList(_context.Aviones, "AvionId", "AvionId", vuelo.AvionId);
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", vuelo.DestinoId);
            return View(vuelo);
        }

        // POST: Vueloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VueloId,AvionId,AerolineaId,DestinoId")] Vuelo vuelo)
        {
            if (id != vuelo.VueloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VueloExists(vuelo.VueloId))
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
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", vuelo.AerolineaId);
            ViewData["AvionId"] = new SelectList(_context.Aviones, "AvionId", "AvionId", vuelo.AvionId);
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", vuelo.DestinoId);
            return View(vuelo);
        }

        // GET: Vueloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vuelos == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelos
                .Include(v => v.Aerolinea)
                .Include(v => v.Avion)
                .Include(v => v.Destino)
                .FirstOrDefaultAsync(m => m.VueloId == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // POST: Vueloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vuelos == null)
            {
                return Problem("Entity set 'AeropuertoContext.Vuelos'  is null.");
            }
            var vuelo = await _context.Vuelos.FindAsync(id);
            if (vuelo != null)
            {
                _context.Vuelos.Remove(vuelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VueloExists(int id)
        {
          return (_context.Vuelos?.Any(e => e.VueloId == id)).GetValueOrDefault();
        }
    }
}
