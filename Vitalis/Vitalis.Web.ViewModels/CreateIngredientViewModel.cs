using System.ComponentModel.DataAnnotations;
using Vitalis.GCommon;
using Vitalis.Web.ViewModels;

namespace Vitalis.Web.ViewModels
{
    public class CreateIngredientViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.IngredientNameMaxLength)]
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }

        [MaxLength(ValidationConstants.IngredientNotesMaxLength)]
        public string? Notes { get; set; }

        public NutrientProfileViewModel NutrientProfile { get; set; } = null!;

        public List<TagInputViewModel>? TagInputs { get; set; }
    }
}
