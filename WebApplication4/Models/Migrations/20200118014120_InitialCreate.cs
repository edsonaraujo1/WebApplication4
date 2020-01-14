using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication4.Models.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCli = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Contrato = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor_principal = table.Column<decimal>(nullable: false),
                    Valor_atualizado = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCli);
                });

            migrationBuilder.CreateTable(
                name: "Debito",
                columns: table => new
                {
                    IdDeb = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vencimento = table.Column<DateTime>(nullable: false),
                    Valor_inicial = table.Column<decimal>(nullable: false),
                    Valor_final = table.Column<decimal>(nullable: false),
                    Juros = table.Column<decimal>(nullable: false),
                    Multa = table.Column<decimal>(nullable: false),
                    Dias = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debito", x => x.IdDeb);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    IdPagto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCli = table.Column<int>(nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Contrato = table.Column<string>(nullable: true),
                    Parcela = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Situacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.IdPagto);
                    table.ForeignKey(
                        name: "FK_Pagamento_Clientes_IdCli",
                        column: x => x.IdCli,
                        principalTable: "Clientes",
                        principalColumn: "IdCli",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_IdCli",
                table: "Pagamento",
                column: "IdCli");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Debito");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
