using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneBE.Migrations
{
    public partial class AddDeleteVideoAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteVideoDate",
                table: "Flight",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeleteVideoUserId",
                table: "Flight",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3fede301-b2aa-4547-aa85-67c0786f6659");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "87a9da8f-78a0-417b-890b-a494b48fe096");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "b27ff3c9-f87f-43fd-bd5a-3f9157d5ce20");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_DeleteVideoUserId",
                table: "Flight",
                column: "DeleteVideoUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_AspNetUsers_DeleteVideoUserId",
                table: "Flight",
                column: "DeleteVideoUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_AspNetUsers_DeleteVideoUserId",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_Flight_DeleteVideoUserId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "DeleteVideoDate",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "DeleteVideoUserId",
                table: "Flight");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c3e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "ca7d378a-6159-4c5c-9832-01a48b45fd20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "cf011238-092d-4e12-b0a1-a93ea2b34899");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c5e154e-3b0e-446f-86af-483d54fd7210",
                column: "ConcurrencyStamp",
                value: "b1b22d2b-94ef-4a17-8562-012e8c515557");
        }
    }
}
