using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Migrations
{
    /// <inheritdoc />
    public partial class Added_Fields_to_Ordini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Prezzo",
                table: "AppOrdini",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Stato",
                table: "AppOrdini",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prezzo",
                table: "AppOrdini");

            migrationBuilder.DropColumn(
                name: "Stato",
                table: "AppOrdini");
        }
    }
}
