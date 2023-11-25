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
    public class AerolineasController : Controller
    {
        private readonly AeropuertoContext _context;

        public AerolineasController(AeropuertoContext context)
        {
            _context = context;
        }

        // GET: Aerolineas
        public async Task<IActionResult> Index()
        {
              return _context.Aerolineas != null ? 
                          View(await _context.Aerolineas.ToListAsync()) :
                          Problem("Entity set 'AeropuertoContext.Aerolineas'  is null.");
        }

        // GET: Aerolineas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aerolineas == null)
            {
                return NotFound();
            }

            var aerolinea = await _context.Aerolineas
                .FirstOrDefaultAsync(m => m.AerolineaId == id);
            if (aerolinea == null)
            {
                return NotFound();
            }

            return View(aerolinea);
        }

        public JsonResult GetAerolineas()
        {
            var aerolineas = _context.Aerolineas.Select(a => new { a.AerolineaId, a.Nombre }).ToList();
            return Json(aerolineas);
        }

        // GET: Aerolineas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aerolineas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AerolineaId,Nombre")] Aerolinea aerolinea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aerolinea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aerolinea);
        }

        // GET: Aerolineas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aerolineas == null)
            {
                return NotFound();
            }

            var aerolinea = await _context.Aerolineas.FindAsync(id);
            if (aerolinea == null)
            {
                return NotFound();
            }
            return View(aerolinea);
        }

        // POST: Aerolineas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AerolineaId,Nombre")] Aerolinea aerolinea)
        {
            if (id != aerolinea.AerolineaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aerolinea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AerolineaExists(aerolinea.AerolineaId))
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
            return View(aerolinea);
        }

        // GET: Aerolineas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aerolineas == null)
            {
                return NotFound();
            }

            var aerolinea = await _context.Aerolineas
                .FirstOrDefaultAsync(m => m.AerolineaId == id);
            if (aerolinea == null)
            {
                return NotFound();
            }

            return View(aerolinea);
        }

        // POST: Aerolineas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aerolineas == null)
            {
                return Problem("Entity set 'AeropuertoContext.Aerolineas'  is null.");
            }
            var aerolinea = await _context.Aerolineas.FindAsync(id);
            if (aerolinea != null)
            {
                _context.Aerolineas.Remove(aerolinea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AerolineaExists(int id)
        {
          return (_context.Aerolineas?.Any(e => e.AerolineaId == id)).GetValueOrDefault();
        }
    }
}
