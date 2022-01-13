#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProjetoFinalContext _context;

        public ProdutosController(ProjetoFinalContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var listaProdutos = await _context.Produtos.Include(p => p.Departamento).ToListAsync();
            return View(listaProdutos);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantidade,Preco,DepartamentoId")] Produtos produtos)
        {

            _context.Add(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id", produtos.DepartamentoId);
            return View(produtos);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Produtos produtos)
        {
            if (id != produtos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutosExists(produtos.Id))
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
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> FindAllAsync()
        {


            var list = await _context.Produtos.Include(obj => obj.Departamento).ToListAsync();

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Remover(int id)
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Name");
            var produtos = await _context.Produtos
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(produtos);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remover(Produtos produtos, int QuantidadeRemove)
        {

            produtos.RemoverQuantidade(QuantidadeRemove);
            _context.Update(produtos);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> Adicionar(int id)
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Name");
            var produtos = await _context.Produtos
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(produtos);

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Adicionar(Produtos produtos, int quantidadeAdd)
        {

            produtos.AdicionarQuantidade(quantidadeAdd);
            _context.Update(produtos);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
