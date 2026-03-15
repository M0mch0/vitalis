using Vitalis.Web.ViewModels;

namespace Vitalis.Web.ViewModels
{
    public class ViewTagViewModel
    {
        public string? Name { get; set; }

        public int Id { get; set; }
        public IEnumerable<IngredientViewModel>? Ingredients { get; set; }

        public IEnumerable<MealViewModel>? Meals { get; set; }
    }
}
