using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Data.Migrations
{
    public partial class Funds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funds",
                columns: table => new
                {
                    FundID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerID = table.Column<string>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    isCollect = table.Column<bool>(nullable: false),
                    TotalFund = table.Column<int>(nullable: false),
                    UpatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funds", x => x.FundID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funds");
        }
    }
}
