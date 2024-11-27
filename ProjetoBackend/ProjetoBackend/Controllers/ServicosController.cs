using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoBackend.Data;
using ProjetoBackend.Models;

namespace ProjetoBackend.Controllers
{
    public class ServicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            var servicos = await _context.Servicos.ToListAsync();
            if (servicos == null || !servicos.Any())
            {
                return View(new List<Servico>()); // Retorna uma lista vazia para evitar erros
            }

            return View(servicos.OrderBy(s => s.Nome));
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicoId,Nome,ValorServico")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.ServicoId = Guid.NewGuid(); // Gera um novo GUID para o serviço
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ServicoId,Nome,ValorServico")] Servico servico)
        {
            if (id != servico.ServicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.ServicoId))
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

            return View(servico);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Verifica se o serviço existe
        private bool ServicoExists(Guid id)
        {
            return _context.Servicos.Any(e => e.ServicoId == id);
        }

        // Adicione este endpoint na sua controller
        // GET: Servicos/Search?nome={ServicoName}
        public async Task<IActionResult> Search(string nome)
        {
            if (string.IsNullOrEmpty(nome)) // Verifica se o termo de busca está vazio
            {
                return RedirectToAction(nameof(Index)); // Redireciona para o Index principal
            }

            var servicos = await _context.Servicos
                .Where(s => s.Nome.Contains(nome)) // Filtra pelos nomes que contêm o termo
                .ToListAsync();

            return View("Index", servicos.OrderBy(s => s.Nome)); // Reutiliza a View "Index" com os dados filtrados
        }
    }
}
