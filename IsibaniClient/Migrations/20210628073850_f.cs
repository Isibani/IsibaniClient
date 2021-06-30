using Microsoft.EntityFrameworkCore.Migrations;

namespace IsibaniClient.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amountspent",
                table: "ClientProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "ClientProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Estimatedbudget",
                table: "ClientProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amountspent",
                table: "ClientProducts");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ClientProducts");

            migrationBuilder.DropColumn(
                name: "Estimatedbudget",
                table: "ClientProducts");
        }
    }
}
