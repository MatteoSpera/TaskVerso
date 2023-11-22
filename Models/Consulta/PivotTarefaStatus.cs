namespace TaskVerso.Models.Consulta
{
	public class PivotTarefaStatus
	{
		public int Id { get; set; }
		public string Categoria { get; set; }
		public int Concluidas { get; set; }
		public int Pendentes { get; set; }
	}
}
