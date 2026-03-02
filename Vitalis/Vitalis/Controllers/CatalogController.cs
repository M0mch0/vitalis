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
        public IActionResult Ingredients()
        {
            return View();
        }
        public IActionResult Tags()
        {
            return View();
        }

    }
}
