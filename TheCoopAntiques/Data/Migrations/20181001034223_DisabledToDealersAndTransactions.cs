using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCoopAntiques.Data.Migrations
{
    public partial class DisabledToDealersAndTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "TransactionTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Dealers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "TransactionTypes");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Dealers");
        }
    }
}