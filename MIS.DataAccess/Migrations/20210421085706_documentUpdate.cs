using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.DataAccess.Migrations
{
    public partial class documentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CriminalRecordId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CriminalRecordId",
                table: "Documents",
                column: "CriminalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_CriminalRecords_CriminalRecordId",
                table: "Documents",
                column: "CriminalRecordId",
                principalTable: "CriminalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_CriminalRecords_CriminalRecordId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_CriminalRecordId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CriminalRecordId",
                table: "Documents");
        }
    }
}
