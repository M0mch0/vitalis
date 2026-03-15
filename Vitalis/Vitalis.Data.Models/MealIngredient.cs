using System.ComponentModel.DataAnnotations;
using Vitalis.Data;
using Vitalis.GCommon;
namespace Vitalis.Data.Models
{
    public class MealIngredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; } = null!;

        [Required]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; } = null!;

        [Required]
        [Range(ValidationConstants.MealIngredientMinQuantity, ValidationConstants.MealIngredientMaxQuantity)]
        public double Quantity { get; set; }
    }
}
