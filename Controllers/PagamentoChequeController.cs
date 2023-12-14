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
    public class PagamentoChequeController : Controller
    {
        private readonly MyDbVendas _context;

        public PagamentoChequeController(MyDbVendas context)
        {
            _context = context;
        }

        // GET: PagamentoCheque
        public async Task<IActionResult> Index()
        {
              return _context.PagamentoCheques != null ? 
                          View(await _context.PagamentoCheques.ToListAsync()) :
                          Problem("Entity set 'MyDbVendas.PagamentoCheques'  is null.");
        }

        // GET: PagamentoCheque/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PagamentoCheques == null)
            {
                return NotFound();
            }

            var pagamentoCheque = await _context.PagamentoCheques
                .FirstOrDefaultAsync(m => m.PagamentoChequeId == id);
            if (pagamentoCheque == null)
            {
                return NotFound();
            }

            return View(pagamentoCheque);
        }

        // GET: PagamentoCheque/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PagamentoCheque/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagamentoChequeId,Banco,NomeBanco,NomeDoCobrado,InformacoesAdicionais")] PagamentoCheque pagamentoCheque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamentoCheque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pagamentoCheque);
        }

        // GET: PagamentoCheque/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PagamentoCheques == null)
            {
                return NotFound();
            }

            var pagamentoCheque = await _context.PagamentoCheques.FindAsync(id);
            if (pagamentoCheque == null)
            {
                return NotFound();
            }
            return View(pagamentoCheque);
        }

        // POST: PagamentoCheque/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagamentoChequeId,Banco,NomeBanco,NomeDoCobrado,InformacoesAdicionais")] PagamentoCheque pagamentoCheque)
        {
            if (id != pagamentoCheque.PagamentoChequeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamentoCheque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentoChequeExists(pagamentoCheque.PagamentoChequeId))
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
            return View(pagamentoCheque);
        }

        // GET: PagamentoCheque/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PagamentoCheques == null)
            {
                return NotFound();
            }

            var pagamentoCheque = await _context.PagamentoCheques
                .FirstOrDefaultAsync(m => m.PagamentoChequeId == id);
            if (pagamentoCheque == null)
            {
                return NotFound();
            }

            return View(pagamentoCheque);
        }

        // POST: PagamentoCheque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PagamentoCheques == null)
            {
                return Problem("Entity set 'MyDbVendas.PagamentoCheques'  is null.");
            }
            var pagamentoCheque = await _context.PagamentoCheques.FindAsync(id);
            if (pagamentoCheque != null)
            {
                _context.PagamentoCheques.Remove(pagamentoCheque);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentoChequeExists(int id)
        {
          return (_context.PagamentoCheques?.Any(e => e.PagamentoChequeId == id)).GetValueOrDefault();
        }
    }
}
