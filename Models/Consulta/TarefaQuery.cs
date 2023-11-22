namespace TaskVerso.Models.Consulta
{
	public class TarefaQuery
	{
		public int Id { get; set; }
		public string Descricao { get; set; }
		public bool Status { get; set; }
		public string Categoria { get; set; }
		public string Prioridade { get; set; }
		public string Funcionario { get; set; }
	}
}
