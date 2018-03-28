using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ScimMicroservice.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Users_UserId",
                table: "Email");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Email",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "CanNotChangePassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Locked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MustChangePasswordAtNextLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordExpiresOn",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Email",
                newName: "Emails");

            migrationBuilder.RenameColumn(
                name: "PasswordNeverExpires",
                table: "Users",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_Email_UserId",
                table: "Emails",
                newName: "IX_Emails_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emails",
                table: "Emails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emails",
                table: "Emails");

            migrationBuilder.RenameTable(
                name: "Emails",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Users",
                newName: "PasswordNeverExpires");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_UserId",
                table: "Email",
                newName: "IX_Email_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "CanNotChangePassword",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MustChangePasswordAtNextLogin",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordExpiresOn",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Email",
                table: "Email",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Users_UserId",
                table: "Email",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
