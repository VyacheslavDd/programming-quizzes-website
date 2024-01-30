using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgQuizWebsite.Migrations
{
    /// <inheritdoc />
    public partial class subcategoriesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageCategoryId",
                table: "Subcategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_LanguageCategoryId",
                table: "Subcategories",
                column: "LanguageCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_LanguageCategoryId",
                table: "Subcategories",
                column: "LanguageCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_LanguageCategoryId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_LanguageCategoryId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "LanguageCategoryId",
                table: "Subcategories");
        }
    }
}
