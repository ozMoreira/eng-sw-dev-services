using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace orders_microservice.Migrations
{
    /// <inheritdoc />
    public partial class user_id_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "orders",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "CustomerId");
        }
    }
}
