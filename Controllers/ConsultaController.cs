using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskVerso.Models;
using TaskVerso.Models.Consulta;

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
			IEnumerable<TarefaQuery> tarefas = new List<TarefaQuery>();
			if (filtro == null)
			{
				tarefas = from item in contexto.Tarefas
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
				tarefas = from item in contexto.Tarefas
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
	}
}
