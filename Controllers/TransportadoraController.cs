using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace MarcosAraujo.Controllers
{
    public class TransportadoraController : Controller
    {
        private readonly MyDbVendas _context;

        public TransportadoraController(MyDbVendas context)
        {
            _context = context;
        }

        // GET: Transportadora
        public async Task<IActionResult> Index()
        {
              return _context.Transportadoras != null ? 
                          View(await _context.Transportadoras.OrderBy(x => x.Nome).AsNoTracking().ToListAsync()) :
                          Problem("Entity set 'MyDbVendas.Transportadoras'  is null.");
        }

        // GET: Transportadora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transportadoras == null)
            {
                return NotFound();
            }

            var transportadora = await _context.Transportadoras
                .FirstOrDefaultAsync(m => m.TransportadoraId == id);
            if (transportadora == null)
            {
                return NotFound();
            }

            return View(transportadora);
        }

        // GET: Transportadora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transportadora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransportadoraId,Nome")] Transportadora transportadora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transportadora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transportadora);
        }

        // GET: Transportadora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transportadoras == null)
            {
                return NotFound();
            }

            var transportadora = await _context.Transportadoras.FindAsync(id);
            if (transportadora == null)
            {
                return NotFound();
            }
            return View(transportadora);
        }

        // POST: Transportadora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransportadoraId,Nome")] Transportadora transportadora)
        {
            if (id != transportadora.TransportadoraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportadora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportadoraExists(transportadora.TransportadoraId))
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
            return View(transportadora);
        }

        // GET: Transportadora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transportadoras == null)
            {
                return NotFound();
            }

            var transportadora = await _context.Transportadoras
                .FirstOrDefaultAsync(m => m.TransportadoraId == id);
            if (transportadora == null)
            {
                return NotFound();
            }

            return View(transportadora);
        }

        // POST: Transportadora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transportadoras == null)
            {
                return Problem("Entity set 'MyDbVendas.Transportadoras'  is null.");
            }
            var transportadora = await _context.Transportadoras.FindAsync(id);
            if (transportadora != null)
            {
                _context.Transportadoras.Remove(transportadora);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportadoraExists(int id)
        {
          return (_context.Transportadoras?.Any(e => e.TransportadoraId == id)).GetValueOrDefault();
        }
    }
}
