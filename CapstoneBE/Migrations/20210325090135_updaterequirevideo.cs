using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class updaterequirevideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Video",
                table: "Flight",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "953cf865-c701-4397-a853-340b29a25d7b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "68beae30-44c7-428a-aa5a-2df894db7c2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "9afa4137-011d-4ec7-a0bf-c31175f11f56");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Video",
                table: "Flight",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7e860538-c062-4444-a329-2a722d05bbb6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "d1beb651-66d1-4b63-a324-dffdd53ea134");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "d19c928f-2293-45ea-8ef2-90d4970417e1");
        }
    }
}
