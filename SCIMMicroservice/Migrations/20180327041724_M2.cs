using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ScimMicroservice.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MailingAddressId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailingAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    Formatted = table.Column<string>(nullable: true),
                    Locality = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingAddress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MailingAddressId",
                table: "Users",
                column: "MailingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_MailingAddress_MailingAddressId",
                table: "Users",
                column: "MailingAddressId",
                principalTable: "MailingAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_MailingAddress_MailingAddressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "MailingAddress");

            migrationBuilder.DropIndex(
                name: "IX_Users_MailingAddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Users");
        }
    }
}
