using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 18, 15, 32, 2, 175, DateTimeKind.Local).AddTicks(4500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 18, 15, 8, 24, 130, DateTimeKind.Local).AddTicks(8881));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc"), "40ca86d0-621a-4a68-aad8-134a7c16dc2f", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"), new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"), 0, "0f472b4b-7239-44f3-ab90-a3bac7cf1c22", new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "eshop.admin@gmail.com", true, "Admin", "User", false, null, "eshop.admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEA+qxcuuxctvYOiPQTiLX0MQoq7+FfWE7iU3nW0nUQCAKNYTkZzVL099r3xvzGlJTQ==", null, false, "", false, "admin" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"), new Guid("0c13d433-42e4-4162-8a8f-0f2b348d72dc") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5dc2ba10-4647-4476-97c0-d5cfcee1b1f5"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 18, 15, 8, 24, 130, DateTimeKind.Local).AddTicks(8881),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 18, 15, 32, 2, 175, DateTimeKind.Local).AddTicks(4500));

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
                values: new object[] { new DateTime(2020, 8, 18, 15, 8, 24, 160, DateTimeKind.Local).AddTicks(9759), 100 });
        }
    }
}
