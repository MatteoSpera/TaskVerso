using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVerso.Models
{
	[Table("Tarefas")]
	public class Tarefa
	{
		[Key] public int Id { get; set; }

		[Required(ErrorMessage = "É necessária uma Descrição para a Tarefa")] 
		public string Descricao { get; set; }

		[Display(Name = "Concluída")]
		public bool Status {  get; set; }

		[Display(Name = "Categoria")]
		public int categoriaId { get; set; }
		[ForeignKey("categoriaId")]
		public Categoria Categoria { get; set; }

		[Display(Name = "Prioridade")]
		public int prioridadeId { get; set; }
		[ForeignKey("prioridadeId")]
		public Prioridade Prioridade { get; set; }

		[Display(Name = "Funcionario")]
		public int funcionarioId { get; set; }
		[ForeignKey("funcionarioId")]
		public Funcionario Funcionario { get; set; }
	}
}
