using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderingApplicationFigma.Migrations
{
    /// <inheritdoc />
    public partial class food4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Users_Email",
                schema: "FoodOrderingSystem",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UQ_Users_Phone",
                schema: "FoodOrderingSystem",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Phone_Role",
                schema: "FoodOrderingSystem",
                table: "Users",
                columns: new[] { "Email", "Phone", "Role" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Phone_Role",
                schema: "FoodOrderingSystem",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Email",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Phone",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Phone",
                unique: true);
        }
    }
}
