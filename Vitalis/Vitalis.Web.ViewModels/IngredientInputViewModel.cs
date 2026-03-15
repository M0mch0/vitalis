using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.Web.ViewModels
{
    public class IngredientInputViewModel
    {
        public int IngredientId { get; set; }

        public string? IngredientName { get; set; }

        public bool Selected { get; set; }

        public double Quantity { get; set; }
    }
}
