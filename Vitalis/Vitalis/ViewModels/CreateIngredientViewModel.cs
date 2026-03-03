using Vitalis.Models;

namespace Vitalis.ViewModels
{
    public class IngredientTagInput
    {
        public int TagId { get; set; }

        public bool Selected { get; set; }
    }
    public class CreateIngredientViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public NutrientProfile nutrientProfile { get; set; } = new NutrientProfile();

        public List<Tag> Tags { get; set; } = new();

        public List<IngredientTagInput> IngredientTagInputs { get; set; } = new();
    }
}
