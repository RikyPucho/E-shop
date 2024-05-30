using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Migrations
{
    /// <inheritdoc />
    public partial class Added_Carrello : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCarrelli",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumDif = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AppCarrelli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRelazioni",
                columns: table => new
                {
                    IdCarrello = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProdotto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumProdotto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRelazioni", x => new { x.IdCarrello, x.IdProdotto });
                    table.ForeignKey(
                        name: "FK_AppRelazioni_AppCarrelli_IdCarrello",
                        column: x => x.IdCarrello,
                        principalTable: "AppCarrelli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRelazioni_AppProdotti_IdProdotto",
                        column: x => x.IdProdotto,
                        principalTable: "AppProdotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRelazioni_IdCarrello_IdProdotto",
                table: "AppRelazioni",
                columns: new[] { "IdCarrello", "IdProdotto" });

            migrationBuilder.CreateIndex(
                name: "IX_AppRelazioni_IdProdotto",
                table: "AppRelazioni",
                column: "IdProdotto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRelazioni");

            migrationBuilder.DropTable(
                name: "AppCarrelli");
        }
    }
}
