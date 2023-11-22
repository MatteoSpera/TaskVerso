using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		public IActionResult TarefaFuncionario(string busca)
		{
			var trf = contexto.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.Include(tarefa => tarefa.Prioridade)
				.Include(tarefa => tarefa.Funcionario)
				.Where(tarefa => tarefa.Funcionario.Nome.Contains(busca)) //mostra apenas tarefas que estejam atribuidas ao funcionario de id 3
				.ToList();
			return View(trf);
		}
	}
}
