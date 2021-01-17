using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class AddConstraintLocationOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MaintenanceOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceOrder_Location_LocationId",
                table: "MaintenanceOrder",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceOrder_Location_LocationId",
                table: "MaintenanceOrder");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceOrder_LocationId",
                table: "MaintenanceOrder");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MaintenanceOrder");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4552df5b-1e51-4818-aafd-c0836c6a094f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "f56b3344-07fe-4a71-b5ba-aea698b53a5f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "7e953e80-a1b5-4649-be02-88645509aa22");
        }
    }
}
