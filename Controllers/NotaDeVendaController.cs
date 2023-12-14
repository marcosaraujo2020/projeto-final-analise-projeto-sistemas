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
    public class NotaDeVendaController : Controller
    {
        private readonly MyDbVendas _context;

        public NotaDeVendaController(MyDbVendas context)
        {
            _context = context;
        }

        // GET: NotaDeVenda
        public async Task<IActionResult> Index(int? cid)
        {

            if (cid.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(cid);
                if (cliente != null)
                {
                    var notaDeVendas = await _context.NotaDeVendas
                        .Where(v => v.ClienteId == cid)
                        .Include(n => n.Transportadora)
                        .OrderByDescending(x => x.NotaDeVendaId)
                        .AsNoTracking().ToListAsync();
                    
                    ViewBag.Cliente = cliente;
                    return View(notaDeVendas); 
                }
                else
                {
                    return RedirectToAction("Index", "Cliente");
                }
           
            }
            else
            {
                return RedirectToAction("Index", "Cliente");
            }
            
        }



        // GET: NotaDeVenda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NotaDeVendas == null)
            {
                return NotFound();
            }

            var notaDeVenda = await _context.NotaDeVendas
                .Include(n => n.Cliente)
                .Include(n => n.Pagamento)
                .Include(n => n.Transportadora)
                .Include(n => n.Vendedor)
                .FirstOrDefaultAsync(m => m.NotaDeVendaId == id);
            if (notaDeVenda == null)
            {
                return NotFound();
            }

            return View(notaDeVenda);
        }

        // GET: NotaDeVenda/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId");
            ViewData["TransportadoraId"] = new SelectList(_context.Transportadoras, "TransportadoraId", "Nome");
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "VendedorId", "Nome");
            return View();

        }

        // POST: NotaDeVenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaDeVendaId,Data,Tipo,VendedorId,ClienteId,PagamentoId,TransportadoraId")] NotaDeVenda notaDeVenda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notaDeVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", notaDeVenda.ClienteId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId", notaDeVenda.PagamentoId);
            ViewData["TransportadoraId"] = new SelectList(_context.Transportadoras, "TransportadoraId", "TransportadoraId", notaDeVenda.TransportadoraId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "VendedorId", "VendedorId", notaDeVenda.VendedorId);
            return View(notaDeVenda);
        }

        // GET: NotaDeVenda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NotaDeVendas == null)
            {
                return NotFound();
            }

            var notaDeVenda = await _context.NotaDeVendas.FindAsync(id);
            if (notaDeVenda == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", notaDeVenda.ClienteId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId", notaDeVenda.PagamentoId);
            ViewData["TransportadoraId"] = new SelectList(_context.Transportadoras, "TransportadoraId", "TransportadoraId", notaDeVenda.TransportadoraId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "VendedorId", "VendedorId", notaDeVenda.VendedorId);
            return View(notaDeVenda);
        }

        // POST: NotaDeVenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotaDeVendaId,Data,Tipo,VendedorId,ClienteId,PagamentoId,TransportadoraId")] NotaDeVenda notaDeVenda)
        {
            if (id != notaDeVenda.NotaDeVendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notaDeVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaDeVendaExists(notaDeVenda.NotaDeVendaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", notaDeVenda.ClienteId);
            ViewData["PagamentoId"] = new SelectList(_context.Pagamentos, "PagamentoId", "PagamentoId", notaDeVenda.PagamentoId);
            ViewData["TransportadoraId"] = new SelectList(_context.Transportadoras, "TransportadoraId", "TransportadoraId", notaDeVenda.TransportadoraId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "VendedorId", "VendedorId", notaDeVenda.VendedorId);
            return View(notaDeVenda);
        }

        // GET: NotaDeVenda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NotaDeVendas == null)
            {
                return NotFound();
            }

            var notaDeVenda = await _context.NotaDeVendas
                .Include(n => n.Cliente)
                .Include(n => n.Pagamento)
                .Include(n => n.Transportadora)
                .Include(n => n.Vendedor)
                .FirstOrDefaultAsync(m => m.NotaDeVendaId == id);
            if (notaDeVenda == null)
            {
                return NotFound();
            }

            return View(notaDeVenda);
        }

        // POST: NotaDeVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NotaDeVendas == null)
            {
                return Problem("Entity set 'MyDbVendas.NotaDeVendas'  is null.");
            }
            var notaDeVenda = await _context.NotaDeVendas.FindAsync(id);
            if (notaDeVenda != null)
            {
                _context.NotaDeVendas.Remove(notaDeVenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaDeVendaExists(int id)
        {
          return (_context.NotaDeVendas?.Any(e => e.NotaDeVendaId == id)).GetValueOrDefault();
        }
    }
}
