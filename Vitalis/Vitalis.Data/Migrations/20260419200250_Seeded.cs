using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vitalis.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "ImageUrl", "Name", "Notes" },
                values: new object[,]
                {
                    { 1, "https://example.com/images/meals/chicken-rice.jpg", "Grilled Chicken with Rice", "High protein balanced meal" },
                    { 2, "https://example.com/images/meals/salmon-sweetpotato.jpg", "Salmon with Sweet Potato", "Rich in omega-3 and complex carbs" },
                    { 3, "https://example.com/images/meals/oatmeal-blueberries.jpg", "Oatmeal with Blueberries", "Great for breakfast" },
                    { 4, "https://example.com/images/meals/avocado-yogurt.jpg", "Avocado Yogurt Bowl", "Healthy fats and protein combo" }
                });

            migrationBuilder.InsertData(
                table: "NutrientProfiles",
                columns: new[] { "Id", "Carbohydrates", "Fat", "Protein" },
                values: new object[,]
                {
                    { 1, 0, 4, 31 },
                    { 2, 23, 1, 3 },
                    { 3, 7, 0, 3 },
                    { 4, 0, 100, 0 },
                    { 5, 1, 11, 13 },
                    { 6, 0, 13, 20 },
                    { 7, 20, 0, 2 },
                    { 8, 4, 0, 3 },
                    { 9, 4, 5, 10 },
                    { 10, 21, 2, 4 },
                    { 11, 9, 15, 2 },
                    { 12, 22, 49, 21 },
                    { 13, 14, 0, 1 },
                    { 14, 66, 7, 17 }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://example.com/images/tags/protein.png", "Protein" },
                    { 2, "https://example.com/images/tags/carbs.png", "Carbs" },
                    { 3, "https://example.com/images/tags/fats.png", "Fats" },
                    { 4, "https://example.com/images/tags/vegetable.png", "Vegetable" },
                    { 5, "https://example.com/images/tags/fruit.png", "Fruit" },
                    { 6, "https://example.com/images/tags/dairy.png", "Dairy" },
                    { 7, "https://example.com/images/tags/grain.png", "Grain" },
                    { 8, "https://example.com/images/tags/healthy.png", "Healthy" },
                    { 9, "https://example.com/images/tags/meat.png", "Meat" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "ImageUrl", "Name", "Notes", "NutrientProfileId" },
                values: new object[,]
                {
                    { 1, "https://example.com/images/chicken-breast.jpg", "Chicken Breast", "Lean protein source", 1 },
                    { 2, "https://example.com/images/brown-rice.jpg", "Brown Rice", "Whole grain carbohydrate", 2 },
                    { 3, "https://example.com/images/broccoli.jpg", "Broccoli", "Rich in fiber and vitamins", 3 },
                    { 4, "https://example.com/images/olive-oil.jpg", "Olive Oil", "Healthy fats", 4 },
                    { 5, "https://example.com/images/eggs.jpg", "Eggs", "High-quality protein and fats", 5 },
                    { 6, "https://example.com/images/salmon.jpg", "Salmon", "Rich in omega-3 fatty acids", 6 },
                    { 7, "https://example.com/images/sweet-potato.jpg", "Sweet Potato", "Complex carbs and fiber", 7 },
                    { 8, "https://example.com/images/spinach.jpg", "Spinach", "Iron-rich leafy green", 8 },
                    { 9, "https://example.com/images/greek-yogurt.jpg", "Greek Yogurt", "High in protein and probiotics", 9 },
                    { 10, "https://example.com/images/quinoa.jpg", "Quinoa", "Complete protein grain alternative", 10 },
                    { 11, "https://example.com/images/avocado.jpg", "Avocado", "Rich in healthy monounsaturated fats", 11 },
                    { 12, "https://example.com/images/almonds.jpg", "Almonds", "Nutrient-dense snack with healthy fats", 12 },
                    { 13, "https://example.com/images/blueberries.jpg", "Blueberries", "Antioxidant-rich fruit", 13 },
                    { 14, "https://example.com/images/oats.jpg", "Oats", "High in soluble fiber", 14 }
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
                table: "MealIngredients",
                columns: new[] { "Id", "IngredientId", "MealId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 200.0 },
                    { 2, 2, 1, 150.0 },
                    { 3, 3, 1, 100.0 },
                    { 4, 6, 2, 180.0 },
                    { 5, 7, 2, 200.0 },
                    { 6, 8, 2, 80.0 },
                    { 7, 14, 3, 100.0 },
                    { 8, 13, 3, 50.0 },
                    { 9, 9, 3, 150.0 },
                    { 10, 11, 4, 100.0 },
                    { 11, 9, 4, 150.0 },
                    { 12, 12, 4, 30.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 6, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 10, 7 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 11, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 13, 5 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 13, 8 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 14, 2 });

            migrationBuilder.DeleteData(
                table: "IngredientTag",
                keyColumns: new[] { "IngredientsId", "TagsId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MealIngredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "MealTag",
                keyColumns: new[] { "MealsId", "TagsId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "NutrientProfiles",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
