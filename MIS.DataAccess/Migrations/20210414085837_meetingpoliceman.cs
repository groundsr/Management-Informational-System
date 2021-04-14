using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.DataAccess.Migrations
{
    public partial class meetingpoliceman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policemen_Meetings_MeetingId",
                table: "Policemen");

            migrationBuilder.DropIndex(
                name: "IX_Policemen_MeetingId",
                table: "Policemen");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "Policemen");

            migrationBuilder.CreateTable(
                name: "MeetingPolicemen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingPolicemen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingPolicemen_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingPolicemen_Policemen_PolicemanId",
                        column: x => x.PolicemanId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPolicemen_MeetingId",
                table: "MeetingPolicemen",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPolicemen_PolicemanId",
                table: "MeetingPolicemen",
                column: "PolicemanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingPolicemen");

            migrationBuilder.AddColumn<Guid>(
                name: "MeetingId",
                table: "Policemen",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policemen_MeetingId",
                table: "Policemen",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policemen_Meetings_MeetingId",
                table: "Policemen",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
