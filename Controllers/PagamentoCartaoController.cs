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
    public class PagamentoCartaoController : Controller
    {
        private readonly MyDbVendas _context;

        public PagamentoCartaoController(MyDbVendas context)
        {
            _context = context;
        }

        // GET: PagamentoCartao
        public async Task<IActionResult> Index()
        {
              return _context.PagamentoCartaos != null ? 
                          View(await _context.PagamentoCartaos.ToListAsync()) :
                          Problem("Entity set 'MyDbVendas.PagamentoCartaos'  is null.");
        }

        // GET: PagamentoCartao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PagamentoCartaos == null)
            {
                return NotFound();
            }

            var pagamentoCartao = await _context.PagamentoCartaos
                .FirstOrDefaultAsync(m => m.PagamentoCartaoId == id);
            if (pagamentoCartao == null)
            {
                return NotFound();
            }

            return View(pagamentoCartao);
        }

        // GET: PagamentoCartao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PagamentoCartao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagamentoCartaoId,NumeroCartao,Bandeira,NomeDoCobrado,InformacoesAdicionais")] PagamentoCartao pagamentoCartao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pagamentoCartao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pagamentoCartao);
        }

        // GET: PagamentoCartao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PagamentoCartaos == null)
            {
                return NotFound();
            }

            var pagamentoCartao = await _context.PagamentoCartaos.FindAsync(id);
            if (pagamentoCartao == null)
            {
                return NotFound();
            }
            return View(pagamentoCartao);
        }

        // POST: PagamentoCartao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagamentoCartaoId,NumeroCartao,Bandeira,NomeDoCobrado,InformacoesAdicionais")] PagamentoCartao pagamentoCartao)
        {
            if (id != pagamentoCartao.PagamentoCartaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagamentoCartao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagamentoCartaoExists(pagamentoCartao.PagamentoCartaoId))
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
            return View(pagamentoCartao);
        }

        // GET: PagamentoCartao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PagamentoCartaos == null)
            {
                return NotFound();
            }

            var pagamentoCartao = await _context.PagamentoCartaos
                .FirstOrDefaultAsync(m => m.PagamentoCartaoId == id);
            if (pagamentoCartao == null)
            {
                return NotFound();
            }

            return View(pagamentoCartao);
        }

        // POST: PagamentoCartao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PagamentoCartaos == null)
            {
                return Problem("Entity set 'MyDbVendas.PagamentoCartaos'  is null.");
            }
            var pagamentoCartao = await _context.PagamentoCartaos.FindAsync(id);
            if (pagamentoCartao != null)
            {
                _context.PagamentoCartaos.Remove(pagamentoCartao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagamentoCartaoExists(int id)
        {
          return (_context.PagamentoCartaos?.Any(e => e.PagamentoCartaoId == id)).GetValueOrDefault();
        }
    }
}
