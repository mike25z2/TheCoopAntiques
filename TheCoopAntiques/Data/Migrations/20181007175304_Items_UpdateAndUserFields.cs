using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TheCoopAntiques.Data.Migrations
{
    public partial class Items_UpdateAndUserFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "Items",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Items",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DealerFeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsDeduction = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerFeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DealerFees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DealerId = table.Column<int>(nullable: false),
                    PeriodId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DealerFeeTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealerFees_DealerFeeTypes_DealerFeeTypeId",
                        column: x => x.DealerFeeTypeId,
                        principalTable: "DealerFeeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealerFees_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealerFees_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DealerFees_DealerFeeTypeId",
                table: "DealerFees",
                column: "DealerFeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerFees_DealerId",
                table: "DealerFees",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerFees_PeriodId",
                table: "DealerFees",
                column: "PeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerFees");

            migrationBuilder.DropTable(
                name: "DealerFeeTypes");

            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Items");
        }
    }
}