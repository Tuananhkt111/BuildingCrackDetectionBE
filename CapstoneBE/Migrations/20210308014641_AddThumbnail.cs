using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class AddThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageThumbnails",
                table: "Crack",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "'https://bcdsysstorage.blob.core.windows.net/thumbnails/' + CAST([CrackId] AS VARCHAR) + '.jpg'");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "9232a365-70c3-40c9-affc-8f8fec26f02d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4f2344dd-a04b-40c9-932e-c128a9e71378");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "18bac00d-2ba5-4142-b724-0a1964e51f4d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageThumbnails",
                table: "Crack");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "dbf07a08-2213-4a53-aa35-2639decad5f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "874a9d10-a63b-4afb-98a8-cb958f376ee4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "5906c0f0-e978-4ca1-8c1e-8711d56c52d4");
        }
    }
}
