using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ScimMicroservice.Migrations
{
    public partial class M4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Meta_MetaId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Name_NameId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "NameId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MetaId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Meta_MetaId",
                table: "Users",
                column: "MetaId",
                principalTable: "Meta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Name_NameId",
                table: "Users",
                column: "NameId",
                principalTable: "Name",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Meta_MetaId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Name_NameId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "NameId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MetaId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Meta_MetaId",
                table: "Users",
                column: "MetaId",
                principalTable: "Meta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Name_NameId",
                table: "Users",
                column: "NameId",
                principalTable: "Name",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
