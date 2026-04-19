using System.ComponentModel.DataAnnotations;
using Vitalis.Data;
using Vitalis.GCommon;
namespace Vitalis.Data.Models
{
    public class Tag
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }
        public virtual ICollection<IngredientTag> Ingredients { get; set; } = new HashSet<IngredientTag>();
        public virtual ICollection<MealTag> Meals { get; set; } = new HashSet<MealTag>();
    }
}
