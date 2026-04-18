using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.GCommon;

namespace Vitalis.Web.ViewModels
{
    public class IngredientInputViewModel
    {
        [Required]
        public int IngredientId { get; set; }

        [Required]
        public string? IngredientName { get; set; }

        public bool Selected { get; set; }

        public NutrientProfileViewModel NutrientProfile { get; set; } = null!;


        [Required]
        public double Quantity { get; set; }
    }
}
