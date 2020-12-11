using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class RemoveCascadeSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "e62d3b12-49db-4ca0-9f04-a684df85f8f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "15322311-1a6b-41ed-bb59-8736c427d962");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "cc24167b-1226-499a-849d-0750c7413ab0");

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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "79836427-6230-4f9b-8c2b-64fad8858785");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "70f79eb8-14cc-4c23-9f8c-73e72b95215b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "1fef1149-4f2c-4336-aee5-53c07c2398bd");

            migrationBuilder.AddForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack",
                column: "MaintenanceOrderId",
                principalTable: "MaintenanceOrder",
                principalColumn: "MaintenanceOrderId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
