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
    public class DestinoesController : Controller
    {
        private readonly AeropuertoContext _context;

        public DestinoesController(AeropuertoContext context)
        {
            _context = context;
        }

        // GET: Destinoes
        public async Task<IActionResult> Index()
        {
              return _context.Destinos != null ? 
                          View(await _context.Destinos.ToListAsync()) :
                          Problem("Entity set 'AeropuertoContext.Destinos'  is null.");
        }

        // GET: Destinoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Destinos == null)
            {
                return NotFound();
            }

            var destino = await _context.Destinos
                .FirstOrDefaultAsync(m => m.DestinoId == id);
            if (destino == null)
            {
                return NotFound();
            }

            return View(destino);
        }

        // GET: Destinoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DestinoId,NombreCiudad,Pais")] Destino destino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destino);
        }

        // GET: Destinoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Destinos == null)
            {
                return NotFound();
            }

            var destino = await _context.Destinos.FindAsync(id);
            if (destino == null)
            {
                return NotFound();
            }
            return View(destino);
        }

        // POST: Destinoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DestinoId,NombreCiudad,Pais")] Destino destino)
        {
            if (id != destino.DestinoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinoExists(destino.DestinoId))
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
            return View(destino);
        }

        // GET: Destinoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Destinos == null)
            {
                return NotFound();
            }

            var destino = await _context.Destinos
                .FirstOrDefaultAsync(m => m.DestinoId == id);
            if (destino == null)
            {
                return NotFound();
            }

            return View(destino);
        }

        // POST: Destinoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Destinos == null)
            {
                return Problem("Entity set 'AeropuertoContext.Destinos'  is null.");
            }
            var destino = await _context.Destinos.FindAsync(id);
            if (destino != null)
            {
                _context.Destinos.Remove(destino);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinoExists(int id)
        {
          return (_context.Destinos?.Any(e => e.DestinoId == id)).GetValueOrDefault();
        }
    }
}
