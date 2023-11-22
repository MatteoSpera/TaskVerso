using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskVerso.Models
{
	[Table("Tarefas")]
	public class Tarefa
	{
		[Key] public int Id { get; set; }

		[Required(ErrorMessage = "É necessária uma Descrição para a Tarefa")]
		[StringLength(50)]
		public string Descricao { get; set; }

		[Display(Name = "Concluída")]
		public bool Status {  get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		[Display(Name = "Categoria")]
		public int categoriaId { get; set; }
		[ForeignKey("categoriaId")]
		public Categoria Categoria { get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		[Display(Name = "Prioridade")]
		public int prioridadeId { get; set; }
		[ForeignKey("prioridadeId")]
		public Prioridade Prioridade { get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		[Display(Name = "Funcionário")]
		public int funcionarioId { get; set; }
		[ForeignKey("funcionarioId")]
		public Funcionario Funcionario { get; set; }
	}
}
