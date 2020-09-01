using Microsoft.EntityFrameworkCore.Migrations;

namespace JMMTaskApi.Migrations
{
    public partial class AddedProductPriceAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "p_price",
                table: "Product",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "p_price",
                table: "Product");
        }
    }
}
