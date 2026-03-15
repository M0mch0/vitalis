using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Services.Core;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;
namespace Vitalis.Controllers
{
    public class CatalogController : Controller
    {

        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Meals()
        {
            IEnumerable<MealViewModel> meals = await catalogService.GetAllMealsAsync();
            return View(meals);
        }

        
        [HttpGet]
        public async Task<IActionResult> Ingredients()
        {
            IEnumerable<IngredientViewModel> ingredients = await catalogService.GetAllIngredientsAsync();
            return View(ingredients);
             
        }
        
        [HttpGet]
        public async Task<IActionResult> Tags()
        {
            IEnumerable<TagViewModel> tags = await catalogService.GetAllTagsAsync();

            return View(tags);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMeal(int id, int TagId)
        {
            await catalogService.DeleteTagAsync(id);

            if (TagId != 0) return RedirectToAction("ViewTag", new { id =TagId });
            else return RedirectToAction("Meals");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteIngredient(int id, int TagId)
        {
            await catalogService.DeleteIngredientAsync(id);

            if (TagId != 0) return RedirectToAction("ViewTag", new { id = TagId });
            else return RedirectToAction("Ingredients");
        }
        [HttpPost]

        

        [HttpPost]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await catalogService.DeleteTagAsync(id);

            return RedirectToAction("Tags", "Catalog");
        }

        [HttpGet]
        public async Task<IActionResult> ViewTag(int id)
        {
            ViewTagViewModel tagview = await catalogService.GetViewByTagAsync(id);

            return View(tagview);
        }

    }
}
