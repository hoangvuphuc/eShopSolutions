using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangeFileSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc"),
                column: "ConcurrencyStamp",
                value: "526652f1-07ec-4eb5-88e8-72adbec2ee65");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c57e7354-e006-4b77-bc03-b927ced4dc5c", "AQAAAAEAACcQAAAAEMZyAu6VxErt9DbpQ+vrh53u2SyFeskgMw0YlUk1K3BUtlTf3yC5L9+PMvrdhOFWUg==" });

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
                values: new object[] { new DateTime(2020, 8, 20, 15, 21, 38, 8, DateTimeKind.Local).AddTicks(7223), 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

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
        }
    }
}
