using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.GCommon;

namespace Vitalis.Data.Models
{
    public class JournalEntryMeal
    {
        
        public int JournalEntryId { get; set; }
        public int MealId { get; set; }

        [Required]
        [Range(ValidationConstants.MealIngredientMinQuantity, ValidationConstants.MealIngredientMaxQuantity)]
        public double Quantity { get; set; }



        [MaxLength(ValidationConstants.MealNotesMaxLength)]
        public string? Notes { get; set; }

        public virtual JournalEntry JournalEntry { get; set; } = null!;
        public virtual Meal Meal { get; set; } = null!;

    }
}
