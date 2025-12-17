using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderingApplicationFigma.Migrations
{
    /// <inheritdoc />
    public partial class food3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(       
                name: "UQ_Users_Phone",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Users_Phone",
                schema: "FoodOrderingSystem",
                table: "Users");
        }
    }
}
