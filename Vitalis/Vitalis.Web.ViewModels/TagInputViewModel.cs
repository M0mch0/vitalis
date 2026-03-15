using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Web.ViewModels
{
    public class TagInputViewModel
    {
        public int TagId { get; set; }

        public string? Name { get; set; }

        public bool Selected { get; set; }
    }
}
