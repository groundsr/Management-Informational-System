using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.DataAccess.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policemen_CriminalRecords_CriminalRecordId",
                table: "Policemen");

            migrationBuilder.RenameColumn(
                name: "CriminalRecordId",
                table: "Policemen",
                newName: "PolicemanId");

            migrationBuilder.RenameIndex(
                name: "IX_Policemen_CriminalRecordId",
                table: "Policemen",
                newName: "IX_Policemen_PolicemanId");

            migrationBuilder.RenameColumn(
                name: "ScheduledOn",
                table: "MeetingRequests",
                newName: "StartDate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Policemen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "MeetingRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CriminalRecordPolicemen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriminalRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriminalRecordPolicemen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriminalRecordPolicemen_CriminalRecords_CriminalRecordId",
                        column: x => x.CriminalRecordId,
                        principalTable: "CriminalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CriminalRecordPolicemen_Policemen_PolicemanId",
                        column: x => x.PolicemanId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriminalRecordPolicemen_CriminalRecordId",
                table: "CriminalRecordPolicemen",
                column: "CriminalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalRecordPolicemen_PolicemanId",
                table: "CriminalRecordPolicemen",
                column: "PolicemanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policemen_Policemen_PolicemanId",
                table: "Policemen",
                column: "PolicemanId",
                principalTable: "Policemen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policemen_Policemen_PolicemanId",
                table: "Policemen");

            migrationBuilder.DropTable(
                name: "CriminalRecordPolicemen");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Policemen");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "MeetingRequests");

            migrationBuilder.RenameColumn(
                name: "PolicemanId",
                table: "Policemen",
                newName: "CriminalRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Policemen_PolicemanId",
                table: "Policemen",
                newName: "IX_Policemen_CriminalRecordId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "MeetingRequests",
                newName: "ScheduledOn");

            migrationBuilder.AddForeignKey(
                name: "FK_Policemen_CriminalRecords_CriminalRecordId",
                table: "Policemen",
                column: "CriminalRecordId",
                principalTable: "CriminalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
