using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class FKLocationMaintenanceOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaintenanceOrder_LocationId",
                table: "MaintenanceOrder");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9302fdea-4ab7-4d6a-95b7-0d6660c5df5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "bf9854cb-f2ba-4095-a944-75b765ca75fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "d7ec86b0-8f8e-4f7f-b300-746162925222");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceOrder_LocationId",
                table: "MaintenanceOrder",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaintenanceOrder_LocationId",
                table: "MaintenanceOrder");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "107397f5-abde-4258-ae74-9cbd2bb749d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a62e6c66-9df8-44b9-8205-c16815d4d712");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "6453a285-96f5-42bd-86af-41fbfceed071");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceOrder_LocationId",
                table: "MaintenanceOrder",
                column: "LocationId",
                unique: true);
        }
    }
}
