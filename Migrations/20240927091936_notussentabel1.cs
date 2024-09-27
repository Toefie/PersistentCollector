using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMVC.Migrations
{
    /// <inheritdoc />
    public partial class notussentabel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardCollections");

            migrationBuilder.CreateTable(
                name: "CardCollection",
                columns: table => new
                {
                    CardsID = table.Column<int>(type: "int", nullable: false),
                    CollectionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCollection", x => new { x.CardsID, x.CollectionsId });
                    table.ForeignKey(
                        name: "FK_CardCollection_Cards_CardsID",
                        column: x => x.CardsID,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCollection_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardCollection_CollectionsId",
                table: "CardCollection",
                column: "CollectionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardCollection");

            migrationBuilder.CreateTable(
                name: "CardCollections",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCollections", x => new { x.CardId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_CardCollections_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardCollections_CollectionId",
                table: "CardCollections",
                column: "CollectionId");
        }
    }
}
