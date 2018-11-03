using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TheCoopAntiques.Data.Migrations
{
    public partial class AddDealerPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DealerPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DealerId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealerPayments_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Layaways",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Deposit = table.Column<decimal>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layaways", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layaways_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LayawayPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LayAwayId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayawayPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayawayPayments_Layaways_LayAwayId",
                        column: x => x.LayAwayId,
                        principalTable: "Layaways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LayawayPayments_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DealerPayments_DealerId",
                table: "DealerPayments",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_LayawayPayments_LayAwayId",
                table: "LayawayPayments",
                column: "LayAwayId");

            migrationBuilder.CreateIndex(
                name: "IX_LayawayPayments_TransactionTypeId",
                table: "LayawayPayments",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Layaways_OrderId",
                table: "Layaways",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerPayments");

            migrationBuilder.DropTable(
                name: "LayawayPayments");

            migrationBuilder.DropTable(
                name: "Layaways");
        }
    }
}