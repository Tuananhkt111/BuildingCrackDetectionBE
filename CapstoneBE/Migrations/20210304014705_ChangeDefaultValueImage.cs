using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class ChangeDefaultValueImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Crack",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "'https://bcdsysstorage.blob.core.windows.net/crack-images/' + CAST([CrackId] AS VARCHAR) + '.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "CAST([CrackId] AS VARCHAR) + '.png'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "239f1bae-b5ca-47ff-9a80-3bd80b97d9ad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "d52456c1-ebae-46d4-ac33-daef11eee7b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "57b1d8b4-d8f2-4287-9f60-72defd42e14f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Crack",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "CAST([CrackId] AS VARCHAR) + '.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "'https://bcdsysstorage.blob.core.windows.net/crack-images/' + CAST([CrackId] AS VARCHAR) + '.png'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2ffd1fa7-6088-40f2-b91d-07c3227d2b73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b87e06e7-c397-47f4-81c4-9cb5d07a6617");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "e5b9de4e-3982-4a66-b0e9-6b6d3504a928");
        }
    }
}
