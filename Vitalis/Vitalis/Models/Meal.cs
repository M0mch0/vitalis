using System.ComponentModel.DataAnnotations;
using Vitalis.Data;
namespace Vitalis.Models
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

        public virtual ICollection<MealIngredient> Ingredients { get; set; } = new HashSet<MealIngredient>();


        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public NutrientProfile AggregateNutrientProfile()
        {
            double totalCarbs = 0;
            double totalProtein = 0;
            double totalFat = 0;

            foreach (MealIngredient mi in Ingredients)
            {
                NutrientProfile? np = mi.Ingredient?.NutrientProfile;
                if (np == null) continue;

                double factor = mi.Quantity / 100;
                totalCarbs += np.Carbohydrates * factor;
                totalProtein += np.Protein * factor;
                totalFat += np.Fat * factor;
            }

            return new NutrientProfile
            {
                Carbohydrates = (int)Math.Round(totalCarbs),
                Protein = (int)Math.Round(totalProtein),
                Fat = (int)Math.Round(totalFat)
            };
        }
    }
}
 