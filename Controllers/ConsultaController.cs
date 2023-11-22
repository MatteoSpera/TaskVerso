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
	}
}
