using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp1.Data.Migrations
{
    public partial class SplitTotalFunds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionUserID",
                table: "Funds",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TotalFunds",
                columns: table => new
                {
                    TotalID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalFundsValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalFunds", x => x.TotalID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalFunds");

            migrationBuilder.DropColumn(
                name: "TransactionUserID",
                table: "Funds");
        }
    }
}
