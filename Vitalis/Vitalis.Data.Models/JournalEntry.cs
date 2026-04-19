using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Data.Models
{
    public class JournalEntry
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<JournalEntryMeal>? Meals { get; set; } = new HashSet<JournalEntryMeal>();

        public virtual ICollection<JournalEntryIngredient>? Ingredients { get; set; } = new HashSet<JournalEntryIngredient>();
    }
}
