using Microsoft.EntityFrameworkCore.Migrations;

namespace JMMTaskApi.Migrations
{
    public partial class AddedTotalAmountAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "o_totalamount",
                table: "ORDERS",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "o_totalamount",
                table: "ORDERS");
        }
    }
}
