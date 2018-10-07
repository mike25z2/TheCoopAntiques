using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCoopAntiques.Data.Migrations
{
    public partial class ChangeInvoiceNumberToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InvoiceNumber",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
