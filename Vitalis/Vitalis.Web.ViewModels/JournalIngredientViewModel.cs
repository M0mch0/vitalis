using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Web.ViewModels
{
    public class JournalIngredientViewModel
    {
        [Required]
        public int IngredientId { get; set; }

        [Required]
        public string? IngredientName { get; set; }

        public bool Selected { get; set; }

        [Required]
        public double Quantity { get; set; }

        public double Carbs { get; set; }

        public double Protein { get; set; }

        public double Fats { get; set; }
    }
}
