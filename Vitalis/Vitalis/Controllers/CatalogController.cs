using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Data.Models;
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
        public async Task<IActionResult> Meals(string? searchQuery = null, int pageNumber = 1, int pageSize = 9)
        {
            var (meals, totalPages) = await catalogService.GetAllMealsAsync(searchQuery, pageNumber, pageSize);

            ViewData["SearchQuery"] = searchQuery;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
            return View(meals);
        }

        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Ingredients(string? searchQuery = null, int pageNumber = 1, int pageSize = 9)
        {
            var(ingredients, totalPages) = await catalogService.GetAllIngredientsAsync(searchQuery, pageNumber, pageSize);

            ViewData["SearchQuery"] = searchQuery;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
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
            if (id <= 0)
            {
                return BadRequest();
            }
            MealViewModel meal = await catalogService.GetMealByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewIngredient(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            IngredientViewModel ingredient = await catalogService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMeal(int id, int TagId)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await catalogService.DeleteMealAsync(id);
            if (TagId != 0) return RedirectToAction("ViewTag", new { id =TagId });
            else return RedirectToAction("Meals");
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteIngredient(int id, int TagId)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await catalogService.DeleteIngredientAsync(id);

            if (TagId != 0) return RedirectToAction("ViewTag", new { id = TagId });
            else return RedirectToAction("Ingredients");
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            await catalogService.DeleteTagAsync(id);

            return RedirectToAction("Tags", "Catalog");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ViewTag(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            ViewTagViewModel tagview = await catalogService.GetViewByTagAsync(id);
            if (tagview == null)
            {
                return NotFound();
            }
            return View(tagview);
        }

    }
}
