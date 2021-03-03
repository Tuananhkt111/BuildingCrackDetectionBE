using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class DefaultValueImage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Crack",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CAST([CrackId] AS VARCHAR) + '.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "[CrackId] + '.png'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "37b942ca-544d-481b-9f6f-a3c0e38cb0fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b430bd6e-87ce-4094-a9e8-216f1554d33a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "0f9411c2-9e51-4db3-bc8b-ef64bc19a079");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Crack",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "[CrackId] + '.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CAST([CrackId] AS VARCHAR) + '.png'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "6e6b90f0-b05a-4afc-9763-b8f8388293d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9a6246b1-4689-46ec-8913-5335d97d7e77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "1281e990-fcee-455b-b416-56520305f531");
        }
    }
}
