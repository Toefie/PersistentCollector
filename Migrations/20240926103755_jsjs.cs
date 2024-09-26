using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMVC.Migrations
{
    /// <inheritdoc />
    public partial class jsjs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardCollectionId",
                table: "CardCollections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardCollectionId",
                table: "CardCollections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
