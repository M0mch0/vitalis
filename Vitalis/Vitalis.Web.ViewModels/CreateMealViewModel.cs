using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Vitalis.GCommon;
using Vitalis.Web.ViewModels;

namespace Vitalis.Web.ViewModels
{
    public class CreateMealViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.MealNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(ValidationConstants.MealNotesMaxLength)]
        public string? Notes { get; set; }

        public string? ImageUrl { get; set; }

        public IEnumerable<NutrientProfileViewModel>? NutrientProfiles { get; set; }

        public List<IngredientInputViewModel>? IngredientInputs { get; set; }

        public List<TagInputViewModel>? TagInputs { get; set; }
    }
}
