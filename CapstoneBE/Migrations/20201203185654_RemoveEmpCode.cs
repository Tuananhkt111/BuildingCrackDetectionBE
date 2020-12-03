using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class RemoveEmpCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmpCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "78a0aeac-a50d-4480-9f7a-e1a13924381e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "db7c6aa1-4066-47ac-bf61-b2bae6195401");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "6506c88d-9809-419a-9047-4126b9aab720");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmpCode",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "78e1f881-b2bf-47a1-a3a4-1c11397a62bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "c6975ff9-33a4-487e-b9c6-1638aa9457a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "37a31c75-ee20-4a9c-afb7-4099422d3276");
        }
    }
}
