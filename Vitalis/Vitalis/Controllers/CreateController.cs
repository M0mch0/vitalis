using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Models;
using Vitalis.ViewModels;

namespace Vitalis.Controllers
{
    public class CreateController : Controller
    {
        private readonly VitalisDbContext dbContext;

        public CreateController(VitalisDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Meal()
        {
            
            var vm = new CreateMealViewModel
            {
                Ingredients = dbContext.Ingredients
                                .OrderBy(i => i.Name)
                                .AsNoTracking()
                                .ToList(),
                Tags = dbContext.Tags.
                                OrderBy(t => t.Name)
                                .AsNoTracking()
                                .ToList(),
                NutrientProfiles = dbContext.NutrientProfiles
                                .AsNoTracking()
                                .ToList()
            };

            vm.MealIngredientInputs = vm.Ingredients
                .Select(i => new MealIngredientInput { IngredientId = i.Id, Selected = false, Quantity = 0 })
                .ToList();

            vm.MealTagInputs = vm.Tags
                .Select(i => new MealTagInput { TagId = i.Id, Selected = false})
                .ToList();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Meal(CreateMealViewModel vm)
        {
            try
            {
                Meal meal = new Meal
                {
                    Name = vm.Name,
                    Notes = vm.Notes
                };
                
                foreach (MealIngredientInput input in vm.MealIngredientInputs.Where(i => i.Selected))
                {
                    // Option A: attach by id only (fast)
                    MealIngredient mi = new MealIngredient
                    {
                        IngredientId = input.IngredientId,
                        Quantity = input.Quantity
                    };
                    meal.Ingredients.Add(mi);
                }

                List<int> selectedTagIds = vm.MealTagInputs
                                            .Where(t => t.Selected == true)
                                            .Select(t => t.TagId)
                                            .ToList();
                if (selectedTagIds.Count > 0)
                {
                    var tags = await dbContext.Tags.Where(t => selectedTagIds.Contains(t.Id)).ToListAsync();
                    foreach (var tag in tags)
                    {
                        meal.Tags.Add(tag);
                    }
                }
                dbContext.Meals.Add(meal);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Meals","Catalog");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Ingredient()
        {
            var vm = new CreateIngredientViewModel
            {
                Tags = dbContext.Tags.
                                OrderBy(t => t.Name)
                                .AsNoTracking()
                                .ToList(),
                nutrientProfile = new NutrientProfile()
            };
            vm.IngredientTagInputs = vm.Tags
                .Select(i => new IngredientTagInput { TagId = i.Id, Selected = false })
                .ToList();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Ingredient(CreateIngredientViewModel vm)
        {
            try
            {
                Ingredient ing = new Ingredient
                {
                    Name = vm.Name,
                    Notes = vm.Notes,
                    NutrientProfile = vm.nutrientProfile
                    
                };


                List<int> selectedTagIds = vm.IngredientTagInputs
                                            .Where(t => t.Selected == true)
                                            .Select(t => t.TagId)
                                            .ToList();

                if (selectedTagIds.Count > 0)
                {
                    var tags = await dbContext.Tags.Where(t => selectedTagIds.Contains(t.Id)).ToListAsync();
                    foreach (var tag in tags)
                    {
                        ing.Tags.Add(tag);
                    }
                }
                dbContext.Ingredients.Add(ing);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Ingredients", "Catalog");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Tag()
        {
            Tag vm = new Tag();

            return View(vm);
        }
        [HttpPost]
        public IActionResult Tag(Tag tag)
        {

            dbContext.Add(tag);
            dbContext.SaveChanges();
            return RedirectToAction("Tags", "Catalog");
        }
    }
}
