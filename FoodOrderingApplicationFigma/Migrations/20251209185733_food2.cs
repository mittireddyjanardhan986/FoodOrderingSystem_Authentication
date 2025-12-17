using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderingApplicationFigma.Migrations
{
    /// <inheritdoc />
    public partial class food2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTPVerifications",
                schema: "FoodOrderingSystem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OTPVerifications",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    OtpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", unicode: false, maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MobileNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    OTPCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OTPVerification__OtpId", x => x.OtpID);
                    table.ForeignKey(
                        name: "FK__OTPVerification__Customer",
                        column: x => x.CustomerID,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OTPVerifications_CustomerID",
                schema: "FoodOrderingSystem",
                table: "OTPVerifications",
                column: "CustomerID");
        }
    }
}
