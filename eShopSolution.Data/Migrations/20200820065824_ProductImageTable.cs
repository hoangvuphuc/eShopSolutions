using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 18, 15, 32, 2, 175, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc"),
                column: "ConcurrencyStamp",
                value: "ad6dc62e-49ca-4e91-9188-b24474851f1d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "efdc8c81-46f2-4a84-a97a-3b2ce26ee5f5", "AQAAAAEAACcQAAAAEBW2W3nx8wFuCCG7yAV7d3BIOjH5D6kM5Yv7aS6M8/vFjoDiFR/n3ksdETis+ocwcg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ViewCount" },
                values: new object[] { new DateTime(2020, 8, 20, 13, 58, 22, 676, DateTimeKind.Local).AddTicks(6950), 100 });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 18, 15, 32, 2, 175, DateTimeKind.Local).AddTicks(4500),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc"),
                column: "ConcurrencyStamp",
                value: "40ca86d0-621a-4a68-aad8-134a7c16dc2f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f472b4b-7239-44f3-ab90-a3bac7cf1c22", "AQAAAAEAACcQAAAAEA+qxcuuxctvYOiPQTiLX0MQoq7+FfWE7iU3nW0nUQCAKNYTkZzVL099r3xvzGlJTQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "ViewCount" },
                values: new object[] { new DateTime(2020, 8, 18, 15, 32, 2, 206, DateTimeKind.Local).AddTicks(80), 100 });
        }
    }
}
