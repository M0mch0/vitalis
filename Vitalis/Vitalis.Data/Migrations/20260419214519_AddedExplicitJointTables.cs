using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vitalis.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExplicitJointTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientTag");

            migrationBuilder.DropTable(
                name: "MealTag");

            migrationBuilder.CreateTable(
                name: "IngredientTags",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTags", x => new { x.IngredientId, x.TagId });
                    table.ForeignKey(
                        name: "FK_IngredientTags_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTags",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTags", x => new { x.MealId, x.TagId });
                    table.ForeignKey(
                        name: "FK_MealTags_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IngredientTags",
                columns: new[] { "IngredientId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 8 },
                    { 1, 9 },
                    { 2, 2 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 4 },
                    { 3, 8 },
                    { 4, 3 },
                    { 4, 8 },
                    { 5, 1 },
                    { 5, 3 },
                    { 6, 1 },
                    { 6, 3 },
                    { 6, 8 },
                    { 7, 2 },
                    { 7, 4 },
                    { 8, 4 },
                    { 8, 8 },
                    { 9, 1 },
                    { 9, 6 },
                    { 10, 1 },
                    { 10, 2 },
                    { 10, 7 },
                    { 11, 3 },
                    { 11, 8 },
                    { 12, 1 },
                    { 12, 3 },
                    { 13, 5 },
                    { 13, 8 },
                    { 14, 2 },
                    { 14, 7 }
                });

            migrationBuilder.InsertData(
                table: "MealTags",
                columns: new[] { "MealId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 8 },
                    { 2, 1 },
                    { 2, 3 },
                    { 2, 8 },
                    { 3, 2 },
                    { 3, 5 },
                    { 4, 3 },
                    { 4, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientTags_TagId",
                table: "IngredientTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTags_TagId",
                table: "MealTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientTags");

            migrationBuilder.DropTable(
                name: "MealTags");

            migrationBuilder.CreateTable(
                name: "IngredientTag",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTag", x => new { x.IngredientsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_IngredientTag_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTag",
                columns: table => new
                {
                    MealsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTag", x => new { x.MealsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MealTag_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IngredientTag",
                columns: new[] { "IngredientsId", "TagsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 8 },
                    { 1, 9 },
                    { 2, 2 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 4 },
                    { 3, 8 },
                    { 4, 3 },
                    { 4, 8 },
                    { 5, 1 },
                    { 5, 3 },
                    { 6, 1 },
                    { 6, 3 },
                    { 6, 8 },
                    { 7, 2 },
                    { 7, 4 },
                    { 8, 4 },
                    { 8, 8 },
                    { 9, 1 },
                    { 9, 6 },
                    { 10, 1 },
                    { 10, 2 },
                    { 10, 7 },
                    { 11, 3 },
                    { 11, 8 },
                    { 12, 1 },
                    { 12, 3 },
                    { 13, 5 },
                    { 13, 8 },
                    { 14, 2 },
                    { 14, 7 }
                });

            migrationBuilder.InsertData(
                table: "MealTag",
                columns: new[] { "MealsId", "TagsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 8 },
                    { 2, 1 },
                    { 2, 3 },
                    { 2, 8 },
                    { 3, 2 },
                    { 3, 5 },
                    { 4, 3 },
                    { 4, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientTag_TagsId",
                table: "IngredientTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTag_TagsId",
                table: "MealTag",
                column: "TagsId");
        }
    }
}
