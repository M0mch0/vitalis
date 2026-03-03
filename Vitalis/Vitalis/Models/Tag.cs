using System.ComponentModel.DataAnnotations;
using Vitalis.Data;
namespace Vitalis.Models
{
    public class Tag
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
        public virtual ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();
    }
}
