using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskVerso.Models;
using TaskVerso.Models.Consulta;

namespace TaskVerso.Controllers
{
	[Authorize]
    public class TarefasController : Controller
    {
        private readonly Contexto _context;

        public TarefasController(Contexto context)
        {
            _context = context;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Tarefas.Include(t => t.Categoria).Include(t => t.Funcionario).Include(t => t.Prioridade);
            return View(await contexto.ToListAsync());
        }

		public IActionResult grpByCat()
		{
			IEnumerable<TarefaQuery> lstTarefas =
			from item in _context.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.OrderBy(o => o.Categoria)
				.ToList()
			select new TarefaQuery
			{
				Descricao = item.Descricao,
				Categoria = item.Categoria.Nome
			};

			IEnumerable<TrfGrpCat> grpTarefas =
			from item in lstTarefas
			.ToList()
			group item by new { item.Categoria }
			into grupo
			orderby grupo.Count()
			select new TrfGrpCat
			{
				Categoria = grupo.Key.Categoria,
				Quantidade = grupo.Count()

			};


			return View(grpTarefas);
		}

		public IActionResult TarefaFuncionario(string filtro)
		{
			IEnumerable<TarefaQuery> tarefas = new List<TarefaQuery>();
			if (filtro == null)
			{
				tarefas = from item in _context.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.Include(tarefa => tarefa.Prioridade)
				.Include(tarefa => tarefa.Funcionario)
				.OrderBy(o => o.Funcionario)
				.ThenBy(o => o.Categoria)
				.ThenByDescending(o => o.Prioridade)
				.ToList()
				select new TarefaQuery
				{
					Descricao = item.Descricao,
					Status = item.Status,
					Categoria = item.Categoria.Nome,
					Prioridade = item.Prioridade.Nivel,
					Funcionario = item.Funcionario.Nome
				};

			}
			else
			{
				tarefas = from item in _context.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.Include(tarefa => tarefa.Prioridade)
				.Include(tarefa => tarefa.Funcionario)
				.OrderBy(o => o.Funcionario)
				.ThenBy(o => o.Categoria)
				.ThenByDescending(o => o.Prioridade)
				.Where(tarefa => tarefa.Funcionario.Nome.Contains(filtro)) //mostra apenas tarefas que estejam atribuidas ao funcionario buscado
				.ToList()
				select new TarefaQuery
				{
					Descricao = item.Descricao,
					Status = item.Status,
					Categoria = item.Categoria.Nome,
					Prioridade = item.Prioridade.Nivel,
					Funcionario = item.Funcionario.Nome
				};
			}
			return View(tarefas);
		}

		// GET: Tarefas/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Funcionario)
                .Include(t => t.Prioridade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");
            ViewData["prioridadeId"] = new SelectList(_context.Prioridades, "Id", "Nivel");
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Status,categoriaId,prioridadeId,funcionarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
				Funcionario funcionario = await _context.Funcionarios.FindAsync(tarefa.funcionarioId);
				funcionario.Atribuicoes++;
				_context.Update(funcionario);

                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", tarefa.categoriaId);
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", tarefa.funcionarioId);
            ViewData["prioridadeId"] = new SelectList(_context.Prioridades, "Id", "Nivel", tarefa.prioridadeId);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", tarefa.categoriaId);
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", tarefa.funcionarioId);
            ViewData["prioridadeId"] = new SelectList(_context.Prioridades, "Id", "Nivel", tarefa.prioridadeId);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Status,categoriaId,prioridadeId,funcionarioId")] Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {	
					
					Tarefa tarefaOld = await _context.Tarefas.FindAsync(tarefa.Id);
					Funcionario funcionario = await _context.Funcionarios.FindAsync(tarefaOld.funcionarioId);
					funcionario.Atribuicoes--;
					_context.Entry(tarefaOld).State = EntityState.Detached;

					funcionario = await _context.Funcionarios.FindAsync(tarefa.funcionarioId);
					funcionario.Atribuicoes++;

					_context.Update(funcionario);
					_context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.Id))
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
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", tarefa.categoriaId);
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", tarefa.funcionarioId);
            ViewData["prioridadeId"] = new SelectList(_context.Prioridades, "Id", "Nivel", tarefa.prioridadeId);
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarefas == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.Categoria)
                .Include(t => t.Funcionario)
                .Include(t => t.Prioridade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarefas == null)
            {
                return Problem("Entity set 'Contexto.Tarefas'  is null.");
            }
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
				Funcionario funcionario = await _context.Funcionarios.FindAsync(tarefa.funcionarioId);
				funcionario.Atribuicoes--;

				_context.Update(funcionario);
				_context.Tarefas.Remove(tarefa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
          return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}
