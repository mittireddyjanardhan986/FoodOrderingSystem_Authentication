using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderingApplicationFigma.Migrations
{
    /// <inheritdoc />
    public partial class FoodDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FoodOrderingSystem");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A645B282C", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__States__C3BA3B3A6F7E7A4F", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C5B57DA4F", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Users__Roles__RoleID",
                        column: x => x.Role,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cities__F2D21B767040D9CA", x => x.CityId);
                    table.ForeignKey(
                        name: "FK__Cities__StateId__49C3F6B7",
                        column: x => x.StateId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "States",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admins__719FE4886B1779A7", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK__Admins__UserId__3E52440B",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carts__51BCD7B75BF44B1B", x => x.CartId);
                    table.ForeignKey(
                        name: "FK__Carts__UserId__619B8048",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64D8FC64ECDA", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK__Customers__UserI__4222D4EF",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Addresse__091C2AFBF205A080", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK__Addresses__CityI__5629CD9C",
                        column: x => x.CityId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK__Addresses__State__571DF1D5",
                        column: x => x.StateId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "States",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK__Addresses__UserI__5535A963",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Restaura__87454C95DE87DA2E", x => x.RestaurantId);
                    table.ForeignKey(
                        name: "FK__Restauran__CityI__5070F446",
                        column: x => x.CityId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK__Restauran__State__5165187F",
                        column: x => x.StateId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "States",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK__Restauran__UserI__4F7CD00D",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OTPVerifications",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    OtpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    OTPCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerID = table.Column<int>(type: "int", unicode: false, maxLength: 450, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MenuCategories",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuCate__19093A0B49DC95DC", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK__MenuCateg__Resta__59FA5E80",
                        column: x => x.RestaurantId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BCFFEBAB133", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK__Orders__AddressI__6D0D32F4",
                        column: x => x.AddressId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK__Orders__Restaura__6C190EBB",
                        column: x => x.RestaurantId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                    table.ForeignKey(
                        name: "FK__Orders__UserId__6B24EA82",
                        column: x => x.UserId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79CEE15BC039", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK__Reviews__Custome__76969D2E",
                        column: x => x.CustomerId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK__Reviews__Restaur__778AC167",
                        column: x => x.RestaurantId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuItem__8943F7229240A974", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK__MenuItems__Categ__5DCAEF64",
                        column: x => x.CategoryId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "MenuCategories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__488B0B0A60722FBE", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK__CartItems__CartI__656C112C",
                        column: x => x.CartId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Carts",
                        principalColumn: "CartId");
                    table.ForeignKey(
                        name: "FK__CartItems__MenuI__66603565",
                        column: x => x.MenuItemId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "FoodOrderingSystem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__57ED0681851F4B42", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK__OrderItem__MenuI__71D1E811",
                        column: x => x.MenuItemId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId");
                    table.ForeignKey(
                        name: "FK__OrderItem__Order__70DDC3D8",
                        column: x => x.OrderId,
                        principalSchema: "FoodOrderingSystem",
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                schema: "FoodOrderingSystem",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateId",
                schema: "FoodOrderingSystem",
                table: "Addresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                schema: "FoodOrderingSystem",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Admins__1788CC4D4D08B65B",
                schema: "FoodOrderingSystem",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                schema: "FoodOrderingSystem",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MenuItemId",
                schema: "FoodOrderingSystem",
                table: "CartItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                schema: "FoodOrderingSystem",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                schema: "FoodOrderingSystem",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__1788CC4DA3641E4D",
                schema: "FoodOrderingSystem",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_RestaurantId",
                schema: "FoodOrderingSystem",
                table: "MenuCategories",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                schema: "FoodOrderingSystem",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                schema: "FoodOrderingSystem",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "FoodOrderingSystem",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                schema: "FoodOrderingSystem",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                schema: "FoodOrderingSystem",
                table: "Orders",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "FoodOrderingSystem",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OTPVerifications_CustomerID",
                schema: "FoodOrderingSystem",
                table: "OTPVerifications",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CityId",
                schema: "FoodOrderingSystem",
                table: "Restaurants",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_StateId",
                schema: "FoodOrderingSystem",
                table: "Restaurants",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "UQ__Restaura__1788CC4D4F1200E2",
                schema: "FoodOrderingSystem",
                table: "Restaurants",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                schema: "FoodOrderingSystem",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                schema: "FoodOrderingSystem",
                table: "Reviews",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "UQ__States__554763154C99B2EC",
                schema: "FoodOrderingSystem",
                table: "States",
                column: "StateName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_Email",
                schema: "FoodOrderingSystem",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "OTPVerifications",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "MenuItems",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "MenuCategories",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Restaurants",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "States",
                schema: "FoodOrderingSystem");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "FoodOrderingSystem");
        }
    }
}
