using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Migrations
{
    /// <inheritdoc />
    public partial class Added_Ordini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOrdini",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indrizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrdini", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppOrdineProdotti",
                columns: table => new
                {
                    OrdineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdottoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdottoNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrdineProdotti", x => new { x.OrdineId, x.ProdottoId });
                    table.ForeignKey(
                        name: "FK_AppOrdineProdotti_AppOrdini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "AppOrdini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrdineProdotti_AppProdotti_ProdottoId",
                        column: x => x.ProdottoId,
                        principalTable: "AppProdotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrdineProdotti_OrdineId_ProdottoId",
                table: "AppOrdineProdotti",
                columns: new[] { "OrdineId", "ProdottoId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrdineProdotti_ProdottoId",
                table: "AppOrdineProdotti",
                column: "ProdottoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOrdineProdotti");

            migrationBuilder.DropTable(
                name: "AppOrdini");
        }
    }
}
