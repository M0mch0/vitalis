using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.GCommon;
namespace Vitalis.Web.ViewModels
{
    public class NutrientProfileViewModel
    {
        public int Id { get; set; }

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
