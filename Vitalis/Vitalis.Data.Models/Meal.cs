using System.ComponentModel.DataAnnotations;
using Vitalis.Data;
using Vitalis.GCommon;
namespace Vitalis.Data.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.MealNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.MealNotesMaxLength)]
        public string? Notes { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<MealIngredient> Ingredients { get; set; } = new HashSet<MealIngredient>();


        public virtual ICollection<MealTag> Tags { get; set; } = new HashSet<MealTag>();

    }
}
 