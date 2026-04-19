using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Data.Models
{
    public class IngredientTag
    {

        [Required]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; } = null!;

        [Required]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; } = null!;
    }
}
