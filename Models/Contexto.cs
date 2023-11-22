﻿using Microsoft.EntityFrameworkCore;
using TaskVerso.Models.Consulta;

namespace TaskVerso.Models
{
	public class Contexto: DbContext
	{
		public Contexto(DbContextOptions<Contexto> options): base(options) { }

		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Prioridade> Prioridades { get; set; }
		public DbSet<Funcionario> Funcionarios { get; set; }
		public DbSet<Tarefa> Tarefas { get; set; }
		public DbSet<TaskVerso.Models.Consulta.TarefaQuery> TarefaQuery { get; set; }
		public DbSet<TaskVerso.Models.Consulta.FuncionarioPorcent> FuncionarioPorcent { get; set; }
		public DbSet<TaskVerso.Models.Consulta.TrfGrpCat> TrfGrpCat { get; set; }
	}
}
