using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVerso.Models
{
	[Table("Funcionarios")]
	public class Funcionario
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		[StringLength(30)]
		public string Nome { get; set; }

		[InverseProperty("Funcionario")]
		public List<Tarefa> Tarefas { get; set; }

		public int Atribuicoes { get; set; }
	}
}
