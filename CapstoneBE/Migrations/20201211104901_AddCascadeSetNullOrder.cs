using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class AddCascadeSetNullOrder : Migration
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
                value: "523b862c-360d-42ab-a971-21f664bb823c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2d51f250-9ed5-41e8-8b59-320069ccc4a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "2778d93f-c4e7-4560-a47c-dd44a8b8e569");

            migrationBuilder.AddForeignKey(
                name: "FK_Crack_MaintenanceOrder_MaintenanceOrderId",
                table: "Crack",
                column: "MaintenanceOrderId",
                principalTable: "MaintenanceOrder",
                principalColumn: "MaintenanceOrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
