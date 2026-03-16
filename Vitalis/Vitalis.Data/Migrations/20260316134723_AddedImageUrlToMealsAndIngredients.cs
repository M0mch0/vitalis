using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitalis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrlToMealsAndIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Ingredients");
        }
    }
}
