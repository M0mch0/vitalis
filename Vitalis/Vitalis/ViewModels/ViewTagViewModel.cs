using Vitalis.Models;

namespace Vitalis.ViewModels
{
    public class ViewTagViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();

        public List<Meal> Meals { get; set; } = new();


    }
}
