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
        public int Id { get; set; }

        public virtual ICollection<MealInputViewModel> Meals { get; set; } = new HashSet<MealInputViewModel>();

        public virtual ICollection<IngredientInputViewModel> Ingredients { get; set; } = new HashSet<IngredientInputViewModel>();
    }
}
