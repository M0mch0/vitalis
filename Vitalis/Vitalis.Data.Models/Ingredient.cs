using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vitalis.Data;
using Vitalis.GCommon;
namespace Vitalis.Data.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.IngredientNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.IngredientNotesMaxLength)]
        public string? Notes { get; set; }

        [Required]
        [ForeignKey(nameof(NutrientProfile))]
        public int NutrientProfileId { get; set; }

        public virtual NutrientProfile NutrientProfile { get; set; } = null!;

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public virtual ICollection<MealIngredient> Meals { get; set; } = new HashSet<MealIngredient>();
    }
}
