using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KlingerStore.Payment.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "varchar(255)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NameCart = table.Column<string>(type: "varchar(250)", nullable: false),
                    NumberCart = table.Column<string>(type: "varchar(16)", nullable: false),
                    ExpiracaoCart = table.Column<string>(type: "varchar(10)", nullable: false),
                    CvvCart = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Transaction",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusTransaction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Transaction", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_TB_Transaction_TB_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "TB_Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Transaction_PaymentId",
                table: "TB_Transaction",
                column: "PaymentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Transaction");

            migrationBuilder.DropTable(
                name: "TB_Payment");
        }
    }
}
