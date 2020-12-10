using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class RemoveRequiredMaintenanceOrderIdCrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceOrderId",
                table: "Crack",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "34b4b43d-27da-49a6-805b-0d42ca81bbfb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0df81c30-27db-40d0-a644-ec3d8cea30d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "ce0f4aea-8751-4fed-b03a-fda58cf31820");

            migrationBuilder.AddForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack",
                column: "MaintenanceOrderId",
                principalTable: "MaintenanceOrder",
                principalColumn: "MaintenanceOrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceOrderId",
                table: "Crack",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7d90a04b-a6fd-454a-9a82-8b11e826b635");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "fcda3aea-e41c-41b4-97d5-395919bf5348");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "63c852de-1a95-40da-b641-05505dd4190e");

            migrationBuilder.AddForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack",
                column: "MaintenanceOrderId",
                principalTable: "MaintenanceOrder",
                principalColumn: "MaintenanceOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
