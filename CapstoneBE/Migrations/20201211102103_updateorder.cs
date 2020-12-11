using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class updateorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceOrder_MaintenanceWorker_MaintenanceWorkerId",
                table: "MaintenanceOrder");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceWorkerId",
                table: "MaintenanceOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "FK_MaintenanceOrder_MaintenanceWorker_MaintenanceWorkerId",
                table: "MaintenanceOrder",
                column: "MaintenanceWorkerId",
                principalTable: "MaintenanceWorker",
                principalColumn: "MaintenanceWorkerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceOrder_MaintenanceWorker_MaintenanceWorkerId",
                table: "MaintenanceOrder");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceWorkerId",
                table: "MaintenanceOrder",
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
                value: "f5746828-2d5d-4520-96f2-58429aa86b22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "078c74c6-7d14-4c3f-aac5-aab53d3c807a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "856724a6-ee04-49e7-a59c-030851a0c99f");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceOrder_MaintenanceWorker_MaintenanceWorkerId",
                table: "MaintenanceOrder",
                column: "MaintenanceWorkerId",
                principalTable: "MaintenanceWorker",
                principalColumn: "MaintenanceWorkerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
