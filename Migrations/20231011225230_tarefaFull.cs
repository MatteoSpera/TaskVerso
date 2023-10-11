using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskVerso.Migrations
{
    /// <inheritdoc />
    public partial class tarefaFull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: true),
                    prioridadeId = table.Column<int>(type: "int", nullable: true),
                    funcionarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefas_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tarefas_Funcionarios_funcionarioId",
                        column: x => x.funcionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tarefas_Prioridades_prioridadeId",
                        column: x => x.prioridadeId,
                        principalTable: "Prioridades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_categoriaId",
                table: "Tarefas",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_funcionarioId",
                table: "Tarefas",
                column: "funcionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_prioridadeId",
                table: "Tarefas",
                column: "prioridadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Prioridades");
        }
    }
}
