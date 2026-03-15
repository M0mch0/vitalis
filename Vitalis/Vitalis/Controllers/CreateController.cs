using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;

namespace Vitalis.Controllers
{
    public class CreateController : Controller
    {
        private readonly ICatalogService catalogService;

        public CreateController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }


        [HttpGet]
        [Route("Create/Meal")]
        public async Task<IActionResult> Meal()
        {
            CreateMealViewModel vm = await catalogService.GetCreateMealViewModel();
            return View(vm);
        }


        [HttpGet]
        [Route("Create/Meal/{id}")]
        public async Task<IActionResult> Meal(int id)
        {
            CreateMealViewModel vm = await catalogService.GetCreateMealViewModel( id );
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Meal(CreateMealViewModel vm)
        {
            await catalogService.AddMealAsync(vm);

            return RedirectToAction("Meals","Catalog");
        }


        [HttpGet]
        [Route("Create/Ingredient")]
        public async Task<IActionResult> Ingredient()
        {
            CreateIngredientViewModel vm = await catalogService.GetCreateIngredientViewModel();
            return View(vm);
        }


        [HttpGet]
        [Route("Create/Ingredient/{id}")]
        public async Task<IActionResult> Ingredient(int id)
        {
            CreateIngredientViewModel vm = await catalogService.GetCreateIngredientViewModel(id);
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Ingredient(CreateIngredientViewModel vm)
        {
            await catalogService.AddIngredientAsync(vm);
            return RedirectToAction("Ingredients", "Catalog");
        }


        [HttpGet]
        [Route("Create/Tag")]
        public async Task<IActionResult> Tag()
        {
            TagViewModel vm = await catalogService.GetCreateTagViewModel();

            return View(vm);
        }


        [HttpGet]
        [Route("Create/Tag/{id}")]
        public async Task<IActionResult> Tag(int id)
        {
            TagViewModel vm = await catalogService.GetCreateTagViewModel(id);

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Tag(TagViewModel tag)
        {
            await catalogService.AddTagAsync(tag);

            return RedirectToAction("Tags", "Catalog");
        }
        
    }
}
