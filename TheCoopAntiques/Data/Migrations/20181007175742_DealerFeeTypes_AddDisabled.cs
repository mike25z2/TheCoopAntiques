using Microsoft.EntityFrameworkCore.Migrations;

namespace TheCoopAntiques.Data.Migrations
{
    public partial class DealerFeeTypes_AddDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "DealerFeeTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "DealerFeeTypes");
        }
    }
}