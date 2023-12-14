using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;


namespace MarcosAraujo.Controllers
{
    public class ItemController : Controller
    {
        private readonly MyDbVendas _context;

        public ItemController(MyDbVendas context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index(int? ped)
        {
          
            if (ped.HasValue)
            {
                if (_context.NotaDeVendas.Any(p => p.NotaDeVendaId == ped))
                {
                    var venda = await _context.NotaDeVendas
                        .Include(p => p.Cliente)
                        .Include(p => p.Items.OrderBy(i => i.Produto.Nome))
                        .ThenInclude(i => i.Produto)
                        .FirstOrDefaultAsync(p => p.NotaDeVendaId == ped);

                    ViewBag.NotaDeVendas = venda;
                    return View(venda.Items);
                }
                return RedirectToAction("Index", "Cliente");
            }

            return RedirectToAction("Index", "Cliente");
        }

  
        // GET: Item/Create
        public async Task<IActionResult> Create(int? ped, int? prod)
        {                
                  if (ped.HasValue)
                {
                    if (_context.NotaDeVendas.Any(p => p.NotaDeVendaId == ped))
                    {
                        var produtos = _context.Produtos
                            .OrderBy(x => x.Nome)
                            .Select(p => new { p.ProdutoId, NomePreco = $"{p.Nome} ({p.Preco:C})" })
                            .AsNoTracking().ToList();
                        var produtosSelectList = new SelectList(produtos, "ProdutoId", "NomePreco");
                        ViewBag.Produtos = produtosSelectList;

                        if (prod.HasValue && ItemPedidoExiste(ped.Value, prod.Value))
                        {
                            var itemPedido = await _context.Items
                                .Include(i => i.Produto)
                                .FirstOrDefaultAsync(i => i.NotaDeVendaId == ped && i.ProdutoId == prod);
                            return View(itemPedido);
                        }
                        else
                        {
                            return View(new Item()
                            { NotaDeVendaId = ped.Value, Preco = 0, Quantidade = 1 });
                        }
                    }
                    
                    return RedirectToAction("Index", "Cliente");
                }
                
                return RedirectToAction("Index", "Cliente");
        }

            private bool ItemPedidoExiste(int ped, int prod)
            {
                return _context.Items.Any(x => x.NotaDeVendaId == ped && x.ProdutoId == prod);
            }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Item itemPedido)
        {
            if (ModelState.IsValid)
            {
                if (itemPedido.NotaDeVendaId > 0)
                {
                    var produto = await _context.Produtos.FindAsync(itemPedido.ProdutoId);
                    itemPedido.Preco = produto.Preco;
                    if (ItemPedidoExiste(itemPedido.NotaDeVendaId, itemPedido.ProdutoId))
                    {
                        _context.Items.Update(itemPedido);                          
                    }
                    else
                    {
                        _context.Items.Add(itemPedido);
                        
                    }
                  
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { ped = itemPedido.NotaDeVendaId });
                }
                else
                {
                    
                    return RedirectToAction("Index", "Cliente");
                }
            }
            else
            {
                var produtos = _context.Produtos
                        .OrderBy(x => x.Nome)
                        .Select(p => new { p.ProdutoId, NomePreco = $"{p.Nome} ({p.Preco:C})" })
                        .AsNoTracking().ToList();
                var produtosSelectList = new SelectList(produtos, "ProdutoId", "NomePreco");
                ViewBag.Produtos = produtosSelectList;

                return View(itemPedido);
            }
   
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? ped, int? prod)
        {
            if (!ped.HasValue || !prod.HasValue)
            {
                return RedirectToAction("Index", "Cliente");
            }

            if (!ItemPedidoExiste(ped.Value, prod.Value))
            {
                return RedirectToAction("Index", "Cliente");
            }

            var itemPedido = await _context.Items.FindAsync(ped, prod);
            
            return View(itemPedido);
        }

        [HttpPost]
           
        public async Task<IActionResult> Delete(int idPedido, int idProduto)
        {
            var itemPedido = await _context.Items.FindAsync(idPedido, idProduto);
            if (itemPedido != null)
            {
                _context.Items.Remove(itemPedido);
              
                await _context.SaveChangesAsync();
                    
                return RedirectToAction("Index", new { ped = idPedido });
            }
            else
            {
                return RedirectToAction("Index", new { ped = idPedido });
            }
        }

    }     
}

