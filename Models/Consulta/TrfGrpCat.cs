namespace TaskVerso.Models.Consulta
{
	public class TrfGrpCat
	{
		public int Id { get; set; }
		public string Categoria { get; set; }
        public int Quantidade { get; set; }
    }

    public class  TrfGrpCatStts : TrfGrpCat
    {
		public string Status { get; set; }
	}
}
