using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.GCommon;
namespace Vitalis.Web.ViewModels
{
    public class MealViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.MealNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.MealNotesMaxLength)]
        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }

        public IEnumerable<TagInputViewModel>? Tags { get; set; }

        public IEnumerable<IngredientInputViewModel>? Ingredients { get; set; }

    }
}
