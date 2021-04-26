using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.DataAccess.Migrations
{
    public partial class finalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliceSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RootPolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceSections_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policemen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Seniority = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoliceSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policemen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policemen_Policemen_PolicemanId",
                        column: x => x.PolicemanId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policemen_PoliceSections_PoliceSectionId",
                        column: x => x.PoliceSectionId,
                        principalTable: "PoliceSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CriminalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriminalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriminalRecords_Policemen_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "MeetingRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingRequests_Policemen_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicemanMeetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicemanMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicemanMeetings_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicemanMeetings_Policemen_PolicemanId",
                        column: x => x.PolicemanId,
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CriminalRecordPolicemen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicemanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriminalRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateWhenWasAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriminalRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_CriminalRecords_CriminalRecordId",
                        column: x => x.CriminalRecordId,
                        principalTable: "CriminalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_CriminalRecordPolicemen_CriminalRecordId",
                table: "CriminalRecordPolicemen",
                column: "CriminalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalRecordPolicemen_PolicemanId",
                table: "CriminalRecordPolicemen",
                column: "PolicemanId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalRecords_ModifiedById",
                table: "CriminalRecords",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CriminalRecordId",
                table: "Documents",
                column: "CriminalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPolicemen_MeetingId",
                table: "MeetingPolicemen",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPolicemen_PolicemanId",
                table: "MeetingPolicemen",
                column: "PolicemanId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRequestPolicemen_MeetingRequestId",
                table: "MeetingRequestPolicemen",
                column: "MeetingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRequestPolicemen_PolicemanId",
                table: "MeetingRequestPolicemen",
                column: "PolicemanId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRequests_RequesterId",
                table: "MeetingRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicemanMeetings_MeetingId",
                table: "PolicemanMeetings",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicemanMeetings_PolicemanId",
                table: "PolicemanMeetings",
                column: "PolicemanId");

            migrationBuilder.CreateIndex(
                name: "IX_Policemen_PolicemanId",
                table: "Policemen",
                column: "PolicemanId");

            migrationBuilder.CreateIndex(
                name: "IX_Policemen_PoliceSectionId",
                table: "Policemen",
                column: "PoliceSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceSections_AddressId",
                table: "PoliceSections",
                column: "AddressId");

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

            migrationBuilder.DropTable(
                name: "CriminalRecordPolicemen");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "MeetingPolicemen");

            migrationBuilder.DropTable(
                name: "MeetingRequestPolicemen");

            migrationBuilder.DropTable(
                name: "PolicemanMeetings");

            migrationBuilder.DropTable(
                name: "CriminalRecords");

            migrationBuilder.DropTable(
                name: "MeetingRequests");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Policemen");

            migrationBuilder.DropTable(
                name: "PoliceSections");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
