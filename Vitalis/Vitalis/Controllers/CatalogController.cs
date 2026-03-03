using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Models;
using Vitalis.ViewModels;

namespace Vitalis.Controllers
{
    public class CatalogController : Controller
    {
        private readonly VitalisDbContext dbContext;

        public CatalogController(VitalisDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Meals()
        {
            ICollection<Meal> meals = dbContext.Meals
                .Include(a => a.Ingredients)
                .ThenInclude(a => a.Ingredient)
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .ToList();
            
            return View(meals);
        }
        [HttpPost]
        public IActionResult DeleteMeal(int id)
        {
            try
            {
                Meal meal = dbContext.Meals
                                .AsNoTracking()
                                .First(m => m.Id == id);
                if (meal == null)
                    return NotFound();
                dbContext.Remove(meal);
                dbContext.SaveChanges();
                return (RedirectToAction(nameof(Meals)));    
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        public IActionResult Ingredients()
        {
            ICollection<Ingredient> ingredients = dbContext.Ingredients
                .Include(a => a.Tags)
                .Include(a => a.NutrientProfile)
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .ToList();

            return View(ingredients);

        }
        [HttpPost]
        public IActionResult DeleteIngredient(int id)
        {
            try
            {
                Ingredient ing = dbContext.Ingredients
                                .AsNoTracking()
                                .First(i => i.Id == id);
                if (ing == null)
                    return NotFound();
                dbContext.Remove(ing);
                dbContext.SaveChanges();
                return (RedirectToAction(nameof(Ingredients)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        public IActionResult Tags()
        {
            return View();
        }

    }
}
