using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vitalis.Data;
using Vitalis.GCommon;
namespace Vitalis.Data.Models
{
    public class NutrientProfile
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
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
