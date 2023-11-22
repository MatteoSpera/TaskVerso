using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskVerso.Models;

namespace TaskVerso.Controllers
{
	public class ConsultaController : Controller
	{
		private readonly Contexto contexto;
		public ConsultaController(Contexto contexto)
		{
			this.contexto = contexto;
		}

		public IActionResult Filtrar()
		{
			return View();
		}

		public IActionResult TarefaFuncionario(string filtro)
		{
			List<Tarefa> tarefas = new List<Tarefa>();
			if (filtro == null)
			{
				tarefas = contexto.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.Include(tarefa => tarefa.Prioridade)
				.Include(tarefa => tarefa.Funcionario)
				.OrderBy(o => o.Funcionario)
				.ThenBy(o => o.Categoria)
				.ThenByDescending(o => o.Prioridade)
				.ToList();
				
			}
			else
			{
				tarefas = contexto.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.Include(tarefa => tarefa.Prioridade)
				.Include(tarefa => tarefa.Funcionario)
				.OrderBy(o => o.Funcionario)
				.ThenBy(o => o.Categoria)
				.ThenByDescending(o => o.Prioridade)
				.Where(tarefa => tarefa.Funcionario.Nome.Contains(filtro)) //mostra apenas tarefas que estejam atribuidas ao funcionario buscado
				.ToList();
			}
			return View(tarefas);
		}
	}
}
