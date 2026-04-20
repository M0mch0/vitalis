using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.Controllers;
using Vitalis.Web.ViewModels;

namespace Vitalis.Controllers
{
    public class CreateController : BaseController
    {
         private readonly ICreateService createService;

        public CreateController(ICreateService createService)
        {
            this.createService = createService;
        }


        [HttpGet]
        [Route("Create/Meal")]
        public async Task<IActionResult> Meal()
        {
            CreateMealViewModel vm = await createService.GetCreateMealViewModel();
            return View(vm);
        }


        [HttpGet]
        [Route("Create/Meal/{id}")]
        public async Task<IActionResult> Meal(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            CreateMealViewModel vm = await createService.GetCreateMealViewModel( id );
            if(vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Meal(CreateMealViewModel vm)
        {
            await createService.AddMealAsync(vm);

            return RedirectToAction("Meals","Catalog");
        }


        [HttpGet]
        [Route("Create/Ingredient")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Ingredient()
        {
            CreateIngredientViewModel vm = await createService.GetCreateIngredientViewModel();
            return View(vm);
        }


        [HttpGet]
        [Route("Create/Ingredient/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Ingredient(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            CreateIngredientViewModel vm = await createService.GetCreateIngredientViewModel(id);
            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingredient(CreateIngredientViewModel vm)
        {
            await createService.AddIngredientAsync(vm);
            return RedirectToAction("Ingredients", "Catalog");
        }


        [HttpGet]
        [Route("Create/Tag")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Tag()
        {
            TagViewModel vm = await createService.GetCreateTagViewModel();

            return View(vm);
        }


        [HttpGet]
        [Route("Create/Tag/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Tag(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            TagViewModel vm = await createService.GetCreateTagViewModel(id);
            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tag(TagViewModel tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }

            await createService.AddTagAsync(tag);

            return RedirectToAction("Tags", "Catalog");
        }
        
    }
}
