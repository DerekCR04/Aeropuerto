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
    public class PilotoesController : Controller
    {
        private readonly AeropuertoContext _context;

        public PilotoesController(AeropuertoContext context)
        {
            _context = context;
        }

        // GET: Pilotoes
        public async Task<IActionResult> Index()
        {
            var aeropuertoContext = _context.Pilotos.Include(p => p.Aerolinea);
            return View(await aeropuertoContext.ToListAsync());
        }

        // GET: Pilotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pilotos == null)
            {
                return NotFound();
            }

            var piloto = await _context.Pilotos
                .Include(p => p.Aerolinea)
                .FirstOrDefaultAsync(m => m.PilotoId == id);
            if (piloto == null)
            {
                return NotFound();
            }

            return View(piloto);
        }

        // GET: Pilotoes/Create
        public IActionResult Create()
        {
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId");
            return View();
        }

        // POST: Pilotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PilotoId,Nombre,Apellidos,Edad,AerolineaId")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piloto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", piloto.AerolineaId);
            return View(piloto);
        }

        // GET: Pilotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pilotos == null)
            {
                return NotFound();
            }

            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto == null)
            {
                return NotFound();
            }
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", piloto.AerolineaId);
            return View(piloto);
        }

        // POST: Pilotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PilotoId,Nombre,Apellidos,Edad,AerolineaId")] Piloto piloto)
        {
            if (id != piloto.PilotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piloto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotoExists(piloto.PilotoId))
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
            ViewData["AerolineaId"] = new SelectList(_context.Aerolineas, "AerolineaId", "AerolineaId", piloto.AerolineaId);
            return View(piloto);
        }

        // GET: Pilotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pilotos == null)
            {
                return NotFound();
            }

            var piloto = await _context.Pilotos
                .Include(p => p.Aerolinea)
                .FirstOrDefaultAsync(m => m.PilotoId == id);
            if (piloto == null)
            {
                return NotFound();
            }

            return View(piloto);
        }

        // POST: Pilotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pilotos == null)
            {
                return Problem("Entity set 'AeropuertoContext.Pilotos'  is null.");
            }
            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto != null)
            {
                _context.Pilotos.Remove(piloto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilotoExists(int id)
        {
          return (_context.Pilotos?.Any(e => e.PilotoId == id)).GetValueOrDefault();
        }
    }
}
