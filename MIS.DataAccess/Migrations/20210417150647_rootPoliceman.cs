using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.DataAccess.Migrations
{
    public partial class rootPoliceman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RootPolicemanId",
                table: "PoliceSections",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoliceSections_RootPolicemanId",
                table: "PoliceSections",
                column: "RootPolicemanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PoliceSections_Policemen_RootPolicemanId",
                table: "PoliceSections",
                column: "RootPolicemanId",
                principalTable: "Policemen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoliceSections_Policemen_RootPolicemanId",
                table: "PoliceSections");

            migrationBuilder.DropIndex(
                name: "IX_PoliceSections_RootPolicemanId",
                table: "PoliceSections");

            migrationBuilder.DropColumn(
                name: "RootPolicemanId",
                table: "PoliceSections");
        }
    }
}
