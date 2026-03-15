using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.GCommon;
namespace Vitalis.Web.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.TagNameMaxLength)]
        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
