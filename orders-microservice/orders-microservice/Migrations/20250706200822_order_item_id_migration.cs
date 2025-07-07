using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace orders_microservice.Migrations
{
    /// <inheritdoc />
    public partial class order_item_id_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_order_items",
                table: "order_items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_items",
                table: "order_items",
                columns: new[] { "OrderId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_order_items",
                table: "order_items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_items",
                table: "order_items",
                column: "Id");
        }
    }
}
