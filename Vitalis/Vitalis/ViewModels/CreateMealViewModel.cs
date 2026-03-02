using System.Collections.ObjectModel;
using Vitalis.Models;

namespace Vitalis.ViewModels
{
    public class MealIngredientInput
    {
        public int IngredientId { get; set; }
        public bool Selected { get; set; }

        public double Quantity { get; set; }
    }
    public class MealTagInput
    {
        public int TagId { get; set; }

        public bool Selected { get; set; }
    }
    public class CreateMealViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public List<Ingredient> Ingredients { get; set;} = new();

        public List<Tag> Tags { get; set;} = new();

        public List<NutrientProfile> NutrientProfiles { get; set;} = new();

        public List<MealIngredientInput> MealIngredientInputs { get; set;} = new();

        public List<MealTagInput> MealTagInputs { get; set;} = new();
    }
}
