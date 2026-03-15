using Vitalis.Web.ViewModels;

namespace Vitalis.Web.ViewModels
{
    public class CreateIngredientViewModel
    {
        public string Name { get; set; } = null!;

        public int Id { get; set; }
        public string? Notes { get; set; }

        public NutrientProfileViewModel NutrientProfile { get; set; } = null!;

        public List<TagInputViewModel>? TagInputs { get; set; }
    }
}
