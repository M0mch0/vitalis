using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Web.ViewModels
{
    public class TagInputViewModel
    {
        [Required]
        public int TagId { get; set; }

        [Required]

        public string? Name { get; set; }

        public bool Selected { get; set; }
    }
}
