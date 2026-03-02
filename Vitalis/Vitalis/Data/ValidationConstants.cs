namespace Vitalis.Data
{
    public static class ValidationConstants
    {
        // Ingredient
        public const int IngredientNameMaxLength = 100;
        public const int IngredientNotesMaxLength = 2000;

        // Meal
        public const int MealNameMaxLength = 100;
        public const int MealNotesMaxLength = 2000;

        // Tag
        public const int TagNameMaxLength = 50;

        // NutrientProfile
        public const int NutrientMinValue = 0;
        public const int NutrientMaxValue = 100;

        //MealIngredient
        public const double MealIngredientMinQuantity = 0.01;
        public const double MealIngredientMaxQuantity = 10000;
    }
}
