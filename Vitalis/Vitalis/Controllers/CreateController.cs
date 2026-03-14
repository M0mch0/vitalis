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
        [Route("Create/Meal")]
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
        [HttpGet]
        [Route("Create/Meal/{id}")]
        public IActionResult Meal(int id)
        {
            Meal meal = dbContext.Meals.Include(m => m.Tags).Include(m => m.Ingredients).First(i => i.Id == id);
            CreateMealViewModel vm = new CreateMealViewModel
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

            vm.Name = meal.Name;
            vm.Notes = meal.Notes;
            vm.MealIngredientInputs = vm.Ingredients
                .Select(i => new MealIngredientInput { IngredientId = i.Id, Selected = false, Quantity = 0 })
                .ToList();
            vm.MealIngredientInputs
                .Where(mi => meal.Ingredients.Select(t => t.IngredientId).Contains(mi.IngredientId))
                .ToList()
                .ForEach(ti =>
                {
                    ti.Selected = true;
                    ti.Quantity = dbContext.MealIngredients.First(mi => (mi.MealId == id && mi.IngredientId == ti.IngredientId)).Quantity;
                });

            vm.MealTagInputs = vm.Tags
                .Select(i => new MealTagInput { TagId = i.Id, Selected = false })
                .ToList();
            vm.MealTagInputs
                .Where(ti => meal.Tags.Select(t => t.Id).Contains(ti.TagId))
                .ToList()
                .ForEach(ti => ti.Selected = true);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Meal(CreateMealViewModel vm)
        {
            try
            {
                Meal? existingMeal = dbContext.Meals.Include(i => i.Tags).Include(i=>i.Ingredients).FirstOrDefault(i => i.Id == vm.Id);
                var temp = dbContext.Meals.ToList(); 
                if (existingMeal == null)
                {
                    Meal meal = new Meal
                    {
                        Name = vm.Name,
                        Notes = vm.Notes
                    };

                    foreach (MealIngredientInput input in vm.MealIngredientInputs.Where(i => i.Selected))
                    {
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
                }
                else
                {
                    existingMeal.Name = vm.Name;
                    existingMeal.Notes = vm.Notes;

                    List<int>? selectedTagIds = vm.MealTagInputs
                                            .Where(t => t.Selected == true)
                                            .Select(t => t.TagId)
                                            .ToList();
                    List<MealIngredientInput>? selectedIngredients = vm.MealIngredientInputs
                                            .Where(t => t.Selected == true)
                                            .ToList();

                    if (selectedTagIds.Count > 0)
                    {
                        List<Tag> tags = await dbContext.Tags.Where(t => selectedTagIds.Contains(t.Id)).ToListAsync();
                        existingMeal.Tags.Clear();
                        foreach (var tag in tags)
                        {
                            existingMeal.Tags.Add(tag);
                        }
                    }
                    if (selectedIngredients.Count > 0)
                    {
                        List<Ingredient> ingredients = await dbContext.Ingredients.Where(t => selectedIngredients.Select(si => si.IngredientId).Contains(t.Id)).ToListAsync();
                        existingMeal.Ingredients.Clear();
                        dbContext.MealIngredients.Where(mi => ingredients.Select(i=> i.Id).Contains(mi.Id)).ExecuteDelete();
                        foreach (Ingredient ing in ingredients)
                        {
                            double quantity = 0;
                            if (vm.MealIngredientInputs.FirstOrDefault(si => si.IngredientId == ing.Id) != null)
                            {
                                quantity = vm.MealIngredientInputs.First(i => i.IngredientId == ing.Id).Quantity;
                            } 

                            MealIngredient mi = new MealIngredient
                            {
                                IngredientId = ing.Id,
                                Ingredient = ing,
                                Meal = existingMeal,
                                MealId = existingMeal.Id, 
                                Quantity = quantity
                            };
                            dbContext.MealIngredients.Add(mi);
                            //existingMeal.Ingredients.Add(mi);
                            //ing.Meals.Add(mi);
                        }
                    }
                }
                
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Meals","Catalog");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("Create/Ingredient")]
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

        [HttpGet]
        [Route("Create/Ingredient/{id}")]
        public IActionResult Ingredient(int id)
        {
            Ingredient ing = dbContext.Ingredients.Include(i => i.Tags).First(i => i.Id == id);
            var vm = new CreateIngredientViewModel
            {
                Name = ing.Name,
                Tags = dbContext.Tags
                                .OrderBy(t => t.Name)
                                .AsNoTracking()
                                .ToList(),
                nutrientProfile = dbContext.NutrientProfiles
                                .First(n => n.Id == ing.NutrientProfileId)
            };
            vm.IngredientTagInputs = vm.Tags
                .Select(i => new IngredientTagInput { TagId = i.Id, Selected = false })
                .ToList();
            vm.IngredientTagInputs
                .Where(ti => ing.Tags.Select(t=> t.Id).Contains(ti.TagId))
                .ToList()
                .ForEach(ti => ti.Selected = true);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Ingredient(CreateIngredientViewModel vm)
        {
            try
            {
                Ingredient? existingIng = dbContext.Ingredients.Include(i => i.Tags).FirstOrDefault(i=> i.Id == vm.Id);

                if (existingIng == null)
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
                }
                else
                {
                    existingIng.Name = vm.Name;
                    existingIng.Notes = vm.Notes;
                    existingIng.NutrientProfile = vm.nutrientProfile;

                    List<int>? selectedTagIds = vm.IngredientTagInputs
                                            .Where(t => t.Selected == true)
                                            .Select(t => t.TagId)
                                            .ToList();

                    if (selectedTagIds.Count > 0)
                    {
                        var tags = await dbContext.Tags.Where(t => selectedTagIds.Contains(t.Id)).ToListAsync();
                        existingIng.Tags.Clear();
                        foreach (var tag in tags)
                        {
                            existingIng.Tags.Add(tag);
                        }
                    }
                }
                


                
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Ingredients", "Catalog");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Create/Tag")]
        public IActionResult Tag()
        {
            Tag vm = new Tag();

            return View(vm);
        }
        [HttpGet]
        [Route("Create/Tag/{id}")]
        public IActionResult Tag(int id)
        {
            Tag? vm = dbContext.Tags
                        .FirstOrDefault(t => t.Id==id);

            return View(vm);
        }
        [HttpPost]
        public IActionResult Tag(Tag tag)
        {
            Tag? existingTag = dbContext.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existingTag == null)
                dbContext.Add(tag);
            else
            {
                existingTag.Name = tag.Name;
                existingTag.ImageUrl = tag.ImageUrl;
            }
                
            dbContext.SaveChanges();
            
            return RedirectToAction("Tags", "Catalog");
        }
        
    }
}
