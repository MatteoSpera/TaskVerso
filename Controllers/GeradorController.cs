using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskVerso.Models;

namespace TaskVerso.Controllers
{
	[Authorize]
	public class GeradorController : Controller
	{
		private readonly Contexto contexto;

		public GeradorController(Contexto context)
		{
			contexto = context;
		}

		public IActionResult Funcionarios()
		{
			var rand = new Random();
			IEnumerable<string> Nomes = new List<string>
			{
				"Miguel", "Esther", "Bryan", "Emanuelly", "Joaquim", "Rebeca", "Vitor", "Ana", "Thiago", "Lavínia", "Antônio", "Vitória", "Davi", "Bianca", "Francisco", "Catarina", "Enzo", "Larissa", "Bruno", "Maria", "Emanuel", "Fernanda", "Gabriel", "Amanda", "Ian", "Alícia", "Lucas", "Carolina", "Rodrigo", "Agatha", "Otávio", "Gabrielly"
			};

			for(int i = 0; i < 10; i++) 
			{
				int n = contexto.Funcionarios.Count();
				Funcionario funcionario = new Funcionario();
				funcionario.Nome = Nomes.ElementAt(rand.Next(Nomes.Count())); 
				funcionario.Atribuicoes = 0;
				contexto.Funcionarios.Add(funcionario);
			}
			contexto.SaveChanges();

			return RedirectToAction("Index", "Funcionarios");
		}

		public IActionResult Categorias()
		{

			for (int i = 0; i < 10; i++)
			{
				int n = contexto.Categorias.Count();
				Categoria categoria = new Categoria();
				categoria.Nome = "Categoria " + (n + i + 1).ToString(); //cria 10 novas categorias numeradas a partir da quantia ja existente
				contexto.Categorias.Add(categoria);
			}
			contexto.SaveChanges();

			return RedirectToAction("Index", "Categorias");
		}

		public IActionResult Prioridades()
		{
			contexto.Database.ExecuteSqlRaw("delete from prioridades"); // apaga tudo da tabela prioridades
			contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('prioridades', RESEED, 0)"); //reseta a seed 

			string[] vPrioridades = { "Baixa", "Média", "Alta", "Urgente" };

			Prioridade prioridade = new Prioridade();

			foreach(string nivel in vPrioridades)
			{
				prioridade = new Prioridade();
				prioridade.Nivel = nivel;
				contexto.Prioridades.Add(prioridade);
			}

			contexto.SaveChanges();

			return RedirectToAction("Index", "Prioridades");

		}

		public IActionResult Tarefas()
		{
			if(contexto.Prioridades.Any() && contexto.Categorias.Any() && contexto.Funcionarios.Any()) //Só cria tarefas se houver pelo menos um elemento de cada outra classe
			{
				var rand = new Random();
				for (int i = 0; i < 10; i++)
				{
					int n = contexto.Tarefas.Count();
					Tarefa tarefa = new Tarefa();
					tarefa.Descricao = "Tarefa " + (n + i + 1).ToString(); //cria 10 novas tarefas numeradas a partir da quantia ja existente
					tarefa.Status = rand.Next(2) != 0; //gera booleano aleatório
					tarefa.Categoria = contexto.Categorias.ToList().ElementAt(rand.Next(contexto.Categorias.Count())); //Pega Categoria Aleatória da Lista
					tarefa.Funcionario = contexto.Funcionarios.ToList().ElementAt(rand.Next(contexto.Funcionarios.Count())); //Pega Funcionario aleatorio da lista
					Funcionario funcionario = contexto.Funcionarios.Find(tarefa.Funcionario.Id);
					funcionario.Atribuicoes++;

					contexto.Update(funcionario);
					tarefa.Prioridade = contexto.Prioridades.ToList().ElementAt(rand.Next(contexto.Prioridades.Count())); //Pega Prioridade aleatória da lista


					contexto.Tarefas.Add(tarefa);
				}
				contexto.SaveChanges();
				var trf = contexto.Tarefas
				.Include(tarefa => tarefa.Categoria)
				.Include(tarefa => tarefa.Prioridade)
				.Include(tarefa => tarefa.Funcionario)
				.OrderBy(o => o.Id).ToList();
				return RedirectToAction("Index", "Tarefas");
			}
			else
			{
				return View("NoTarefa");
			}

			
			
		}

		public IActionResult LimpaTarefas()
		{
			contexto.Database.ExecuteSqlRaw("delete from tarefas"); // apaga tudo da tabela prioridades
			contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('tarefas', RESEED, 0)"); //reseta a seed 
			contexto.SaveChanges();
			return RedirectToAction("Index", "Tarefas");
		}

		public IActionResult LimpaPrioridades()
		{
			contexto.Database.ExecuteSqlRaw("delete from prioridades"); // apaga tudo da tabela prioridades
			contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('prioridades', RESEED, 0)"); //reseta a seed 
			contexto.SaveChanges();
			return RedirectToAction("Index", "Prioridades");
		}

		public IActionResult LimpaCategorias()
		{
			contexto.Database.ExecuteSqlRaw("delete from categorias"); // apaga tudo da tabela categorias
			contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('categorias', RESEED, 0)"); //reseta a seed 
			contexto.SaveChanges();
			return RedirectToAction("Index", "Categorias");
		}

		public IActionResult LimpaFuncionarios()
		{
			contexto.Database.ExecuteSqlRaw("delete from funcionarios"); // apaga tudo da tabela funcionarios
			contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('funcionarios', RESEED, 0)"); //reseta a seed 
			contexto.SaveChanges();
			return RedirectToAction("Index", "Funcionarios");
		}

		public IActionResult Limpar() 
		{
			LimpaTarefas();
			LimpaPrioridades();
			LimpaCategorias();
			LimpaFuncionarios();
			contexto.SaveChanges();

			return RedirectToAction("Index", "Tarefas");
		}

		public IActionResult Completo()
		{
			this.Limpar();
			this.Funcionarios();
			this.Categorias();
			this.Prioridades();
			this.Tarefas();
			return RedirectToAction("Index", "Tarefas");
		}

	}
}
