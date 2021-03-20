using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class AddFieldExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MaintenanceExpense",
                table: "MaintenanceOrder",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "d4d9e9b1-50c1-4a50-8a22-3a9258a8f12d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "853e868c-6b9a-48f3-87b5-ac60ed92e0fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "c34a40ea-2e39-4f50-90a9-1136c2b0ff90");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceExpense",
                table: "MaintenanceOrder");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3329f462-a389-488f-b6f1-f23db7498a2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "f39e263e-4b53-48d9-aa40-13a329672fd4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "cba13254-876d-4c92-83f7-84c5e374da0f");
        }
    }
}
