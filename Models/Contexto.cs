using Microsoft.EntityFrameworkCore;

namespace TaskVerso.Models
{
	public class Contexto: DbContext
	{
		public Contexto(DbContextOptions<Contexto> options): base(options) { }
	}
}
