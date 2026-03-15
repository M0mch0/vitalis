using System.Collections.ObjectModel;
using Vitalis.Web.ViewModels;

namespace Vitalis.Web.ViewModels
{
    public class CreateMealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }



        public IEnumerable<NutrientProfileViewModel>? NutrientProfiles { get; set; }

        public List<IngredientInputViewModel>? IngredientInputs { get; set; }

        public List<TagInputViewModel>? TagInputs { get; set; }
    }
}
