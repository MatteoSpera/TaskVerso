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
	
		public IActionResult FuncPorcent()
		{
			IEnumerable<FuncionarioPorcent> funcionarios = new List<FuncionarioPorcent>();
			funcionarios = from item in contexto.Funcionarios
			
			.ToList()
			select new FuncionarioPorcent
			{
				Nome = item.Nome,
				Atribuicoes = item.Atribuicoes,
				conclusao = (item.Atribuicoes > 0) ? ((float)contexto.Tarefas.Count(trf => trf.Status && trf.Funcionario == item) / item.Atribuicoes * 100).ToString() + "%" : "NA"
			};
			return View(funcionarios);
		}
	}
}
