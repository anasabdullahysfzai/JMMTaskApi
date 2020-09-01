using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JMMTaskApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    c_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    c_address = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    c_phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__213EE774A73BA1F6", x => x.c_id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    p_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_code = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    p_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    p_stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__82E06B91AA14A403", x => x.p_id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    s_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    s_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    s_address = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    s_phone = table.Column<string>(nullable: true),
                    s_iban = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__2F3684F43611EC1C", x => x.s_id);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    o_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    o_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    o_type = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    c_id = table.Column<int>(nullable: true),
                    s_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ORDERS__904BC20E6DAFDC73", x => x.o_id);
                    table.ForeignKey(
                        name: "FK_ORDERS_CUSTOMER",
                        column: x => x.c_id,
                        principalTable: "Customer",
                        principalColumn: "c_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDERS_SUPPLIERS",
                        column: x => x.s_id,
                        principalTable: "Supplier",
                        principalColumn: "s_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order_Product",
                columns: table => new
                {
                    op_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    op_quantity = table.Column<int>(nullable: false),
                    p_id = table.Column<int>(nullable: false),
                    o_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order_Pr__A26AE2CEA6F14704", x => x.op_id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderProduct",
                        column: x => x.o_id,
                        principalTable: "ORDERS",
                        principalColumn: "o_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product",
                        column: x => x.p_id,
                        principalTable: "Product",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Product_o_id",
                table: "Order_Product",
                column: "o_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Product_p_id",
                table: "Order_Product",
                column: "p_id");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_c_id",
                table: "ORDERS",
                column: "c_id");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_s_id",
                table: "ORDERS",
                column: "s_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_Product");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
