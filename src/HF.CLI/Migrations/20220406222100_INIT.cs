using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF.CLI.Migrations
{
    public partial class INIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sendings",
                columns: table => new
                {
                    RecipientPK = table.Column<string>(type: "TEXT", nullable: false),
                    RecipientAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    SentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransactionHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sendings", x => x.RecipientPK);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sendings");
        }
    }
}
