using System.ComponentModel.DataAnnotations;
using Vitalis.Data;
namespace Vitalis.Models
{
    public class NutrientProfile
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Calories => Carbohydrates * 4 + Protein * 4 + Fat * 9;

        [Required]
        [Range(ValidationConstants.NutrientMinValue, ValidationConstants.NutrientMaxValue)]
        public int Carbohydrates { get; set; }

        [Required]
        [Range(ValidationConstants.NutrientMinValue, ValidationConstants.NutrientMaxValue)]
        public int Protein { get; set; }
        
        [Required]
        [Range(ValidationConstants.NutrientMinValue, ValidationConstants.NutrientMaxValue)]
        public int Fat { get; set; }
    }
}
