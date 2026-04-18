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
    public class IngredientViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.IngredientNameMaxLength)]
        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? Notes { get; set; }

        [Required]
        public NutrientProfileViewModel NutrientProfile { get; set; } = null!;

        public IEnumerable<TagInputViewModel>? Tags { get; set; }
    }
}
