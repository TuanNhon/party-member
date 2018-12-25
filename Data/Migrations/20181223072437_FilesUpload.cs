using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Data.Migrations
{
    public partial class FilesUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesUpload",
                columns: table => new
                {
                    FileID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeetingID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesUpload", x => x.FileID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesUpload");
        }
    }
}
