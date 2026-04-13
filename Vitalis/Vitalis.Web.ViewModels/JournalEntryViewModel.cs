using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Web.ViewModels
{
    public class JournalEntryViewModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        public virtual List<MealInputViewModel>? Meals { get; set; }

        public virtual List<JournalIngredientViewModel>? Ingredients { get; set; }
    }
}
