using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Data.Migrations
{
    public partial class Hope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiedInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EduLevel",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ethnicity",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InComingInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeaveInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MilitaryPoliceRetire",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutGoingInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartyCardNum",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartyDateInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    MeetingID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerID = table.Column<string>(nullable: true),
                    MeetingTitle = table.Column<string>(nullable: true),
                    MeetingDate = table.Column<DateTime>(nullable: true),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.MeetingID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BirthName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiedInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EduLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InComingInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LeaveInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MilitaryPoliceRetire",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OutGoingInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PartyCardNum",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PartyDateInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "AspNetUsers");
        }
    }
}
