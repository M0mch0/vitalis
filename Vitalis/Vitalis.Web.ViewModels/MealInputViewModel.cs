namespace Vitalis.Web.ViewModels
{
    public class MealInputViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public bool Selected { get; set; }

        public int Amount { get; set; }
        public IEnumerable<JournalIngredientViewModel>? Ingredients { get; set; }
    }
}