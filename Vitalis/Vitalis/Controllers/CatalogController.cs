using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Services.Core;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.Controllers;
using Vitalis.Web.ViewModels;
namespace Vitalis.Controllers
{
    public class CatalogController : BaseController
    {

        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Meals(string? searchQuery = null)
        {
            IEnumerable<MealViewModel> meals = await catalogService.GetAllMealsAsync(searchQuery);

            ViewData["SearchQuery"] = searchQuery;
            return View(meals);
        }

        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Ingredients(string? searchQuery = null)
        {
            IEnumerable<IngredientViewModel> ingredients = await catalogService.GetAllIngredientsAsync(searchQuery);

            ViewData["SearchQuery"] = searchQuery;
            return View(ingredients);
             
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Tags()
        {
            IEnumerable<TagViewModel> tags = await catalogService.GetAllTagsAsync();

            return View(tags);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewMeal(int id)
        {
            MealViewModel meal = await catalogService.GetMealByIdAsync(id);
            return View(meal);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewIngredient(int id)
        {
            IngredientViewModel ingredient = await catalogService.GetIngredientByIdAsync(id);
            return View(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMeal(int id, int TagId)
        {
            await catalogService.DeleteMealAsync(id);

            if (TagId != 0) return RedirectToAction("ViewTag", new { id =TagId });
            else return RedirectToAction("Meals");
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteIngredient(int id, int TagId)
        {
            await catalogService.DeleteIngredientAsync(id);

            if (TagId != 0) return RedirectToAction("ViewTag", new { id = TagId });
            else return RedirectToAction("Ingredients");
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await catalogService.DeleteTagAsync(id);

            return RedirectToAction("Tags", "Catalog");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewTag(int id)
        {
            ViewTagViewModel tagview = await catalogService.GetViewByTagAsync(id);

            return View(tagview);
        }

    }
}
