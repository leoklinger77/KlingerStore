using Microsoft.EntityFrameworkCore.Migrations;

namespace KlingerStore.Sales.Data.Migrations
{
    public partial class UpdateImageSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TB_OrderItem",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "TB_OrderItem");
        }
    }
}
