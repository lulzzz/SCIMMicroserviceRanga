using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ScimMicroservice.Migrations
{
    public partial class M9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Users_UserId",
                table: "PhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber");

            migrationBuilder.RenameTable(
                name: "PhoneNumber",
                newName: "PhoneNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_UserId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Users_UserId",
                table: "PhoneNumbers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Users_UserId",
                table: "PhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_UserId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Users_UserId",
                table: "PhoneNumber",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
