using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.DataAccess.Migrations
{
    public partial class requestPoliceman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policemen_MeetingRequests_MeetingRequestId",
                table: "Policemen");

            migrationBuilder.DropIndex(
                name: "IX_Policemen_MeetingRequestId",
                table: "Policemen");

            migrationBuilder.DropColumn(
                name: "MeetingRequestId",
                table: "Policemen");

            migrationBuilder.CreateTable(
                name: "MeetingRequestPolicemen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeetingRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRequestPolicemen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingRequestPolicemen_MeetingRequests_MeetingRequestId",
                        column: x => x.MeetingRequestId,
                        principalTable: "MeetingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingRequestPolicemen_Policemen_PolicemanId",
                        column: x => x.PolicemanId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRequestPolicemen_MeetingRequestId",
                table: "MeetingRequestPolicemen",
                column: "MeetingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRequestPolicemen_PolicemanId",
                table: "MeetingRequestPolicemen",
                column: "PolicemanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingRequestPolicemen");

            migrationBuilder.AddColumn<Guid>(
                name: "MeetingRequestId",
                table: "Policemen",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policemen_MeetingRequestId",
                table: "Policemen",
                column: "MeetingRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policemen_MeetingRequests_MeetingRequestId",
                table: "Policemen",
                column: "MeetingRequestId",
                principalTable: "MeetingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
