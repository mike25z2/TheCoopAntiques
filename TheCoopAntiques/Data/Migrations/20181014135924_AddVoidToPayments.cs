using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCoopAntiques.Data.Migrations
{
    public partial class AddVoidToPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Void",
                table: "LayawayPayments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Void",
                table: "DealerPayments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Void",
                table: "LayawayPayments");

            migrationBuilder.DropColumn(
                name: "Void",
                table: "DealerPayments");
        }
    }
}
