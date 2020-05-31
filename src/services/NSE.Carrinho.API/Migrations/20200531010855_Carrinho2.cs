using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Carrinho.API.Migrations
{
    public partial class Carrinho2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "CarrinhoCliente");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "CarrinhoCliente",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "CarrinhoCliente");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "CarrinhoCliente",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
