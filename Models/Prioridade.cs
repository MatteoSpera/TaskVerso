using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVerso.Models
{
	[Table("Prioridades")]
	public class Prioridade
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(30)]
		public string Nivel { get; set; }

		[InverseProperty("Prioridade")]
		public List<Tarefa> Tarefas { get; set; }
	}
}
