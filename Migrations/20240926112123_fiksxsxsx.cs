using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMVC.Migrations
{
    /// <inheritdoc />
    public partial class fiksxsxsx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Inventories_InventoryID",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CardCollectionId",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "CardCollectionId",
                table: "Cards",
                newName: "CollectionId");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryID",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Inventories_InventoryID",
                table: "Collections",
                column: "InventoryID",
                principalTable: "Inventories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Inventories_InventoryID",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "CollectionId",
                table: "Cards",
                newName: "CardCollectionId");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryID",
                table: "Collections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CardCollectionId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Inventories_InventoryID",
                table: "Collections",
                column: "InventoryID",
                principalTable: "Inventories",
                principalColumn: "ID");
        }
    }
}
