using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;
namespace Vitalis.Services.Core
{
    public class CatalogService : ICatalogService
    {
        private readonly VitalisDbContext context;

        public CatalogService(VitalisDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<MealViewModel>> GetAllMealsAsync()
        {
            return await context.Meals
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .Select(m => new MealViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Notes = m.Notes,
                    Ingredients = m.Ingredients.Select(mi => new IngredientInputViewModel
                    {
                        IngredientId = mi.IngredientId,
                        IngredientName = mi.Ingredient.Name,
                        Quantity = mi.Quantity,
                        Selected = true
                    }).ToList()
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<IngredientViewModel>> GetAllIngredientsAsync()
        {
            return await context.Ingredients
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .Select(i => new IngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Tags = i.Tags.Select(t => new TagInputViewModel
                    {
                        TagId = t.Id,
                        Selected = true,
                        Name = t.Name
                    }),
                    NutrientProfile = new NutrientProfileViewModel
                    {
                        Carbohydrates = i.NutrientProfile.Carbohydrates,
                        Fat = i.NutrientProfile.Fat,
                        Protein = i.NutrientProfile.Protein
                    }
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<TagViewModel>> GetAllTagsAsync()
        {
            return await context.Tags
                .OrderBy(a => a.Name)
                .AsNoTracking()
                .Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl
                })
                .ToListAsync();
        }
        public async Task<ViewTagViewModel> GetViewByTagAsync( int id)
        {
            Tag? tag = await context.Tags
                .FirstOrDefaultAsync(t => t.Id == id);

            if(tag is null)
            {
                throw new Exception($"Tag with id {id} not found.");
            }

            ViewTagViewModel vm = new ViewTagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Ingredients = await context.Ingredients
                    .Where(i => i.Tags.Contains(tag))
                    .Select(i => new IngredientViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        NutrientProfile = new NutrientProfileViewModel
                        {
                            Carbohydrates = i.NutrientProfile.Carbohydrates,
                            Fat = i.NutrientProfile.Fat,
                            Protein = i.NutrientProfile.Protein
                        },
                        Tags = i.Tags.Select(t => new TagInputViewModel
                        {
                            TagId = t.Id,
                            Name = t.Name,
                            Selected = true
                        })
                    })
                    .ToListAsync(),
                Meals = await context.Meals
                    .Where(m => m.Tags.Contains(tag))
                    .Select(m => new MealViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Notes = m.Notes,
                        Ingredients = m.Ingredients.Select(mi => new IngredientInputViewModel
                        {
                            IngredientId = mi.IngredientId,
                            IngredientName = mi.Ingredient.Name,
                            Quantity = mi.Quantity,
                            Selected = true
                        }).ToList(),
                        Tags = m.Tags.Select(t => new TagInputViewModel
                        {
                            TagId = t.Id,
                            Name = t.Name,
                            Selected = true
                        })

                    }).ToListAsync()
            };

            return vm;
        }
        public async Task<CreateMealViewModel> GetCreateMealViewModel()
        {
            CreateMealViewModel vm = new CreateMealViewModel
            {
                IngredientInputs = await context.Ingredients
                                .OrderBy(i => i.Name)
                                .AsNoTracking()
                                .Select(i => new IngredientInputViewModel
                                {
                                    IngredientId = i.Id,
                                    IngredientName = i.Name,
                                    Selected = false,
                                    Quantity = 0
                                })
                                .ToListAsync(),
                TagInputs = await context.Tags.
                                OrderBy(t => t.Name)
                                .AsNoTracking()
                                .Select(i => new TagInputViewModel
                                {
                                    TagId = i.Id,
                                    Name = i.Name,
                                    Selected = false
                                })
                                .ToListAsync()
            };

            return vm;
        }
        public async Task<CreateMealViewModel> GetCreateMealViewModel(int id)
        {
            Meal? meal = await context.Meals
                .Include(m => m.Tags)
                .Include(m => m.Ingredients)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (meal is null)
            {
                throw new Exception($"Meal with id {id} not found.");
            }
            CreateMealViewModel vm = new CreateMealViewModel
            {
                Id = id,
                Name = meal.Name,
                Notes = meal.Notes,
                IngredientInputs = context.Ingredients
                                .OrderBy(i => i.Name)
                                .AsNoTracking()
                                .Select(i => new IngredientInputViewModel
                                {
                                    IngredientId = i.Id,
                                    IngredientName = i.Name,
                                    Selected = false,
                                    Quantity = 0
                                })
                                .ToList(),
                TagInputs = context.Tags.
                                OrderBy(t => t.Name)
                                .AsNoTracking()
                                .Select(t => new TagInputViewModel
                                {
                                    TagId = t.Id,
                                    Name = t.Name,
                                    Selected = false
                                })
                                .ToList()
            };

            meal.Ingredients
                .ToList()
                .ForEach(mi =>
                {
                    var ingredientInput = vm.IngredientInputs.FirstOrDefault(ii => ii.IngredientId == mi.IngredientId);
                    if (ingredientInput != null)
                    {
                        ingredientInput.IngredientId = mi.IngredientId;
                        ingredientInput.IngredientName = context.Ingredients.Find(mi.IngredientId)?.Name;
                        ingredientInput.Selected = true;
                        ingredientInput.Quantity = mi.Quantity;
                    }
                });

            meal.Tags
                .ToList()
                .ForEach(t =>
                {
                    var tagInput = vm.TagInputs.FirstOrDefault(ti => ti.TagId == t.Id);
                    if (tagInput != null)
                    {
                        tagInput.TagId = t.Id;
                        tagInput.Name = context.Tags.Find(t.Id)?.Name;
                        tagInput.Selected = true;
                    }
                });


            return vm;
        }
        public async Task<CreateIngredientViewModel> GetCreateIngredientViewModel()
        {
            CreateIngredientViewModel vm = new CreateIngredientViewModel
            {
                NutrientProfile = new NutrientProfileViewModel
                {
                    Carbohydrates = 0,
                    Fat = 0,
                    Protein = 0
                },
                TagInputs = await context.Tags.
                                OrderBy(t => t.Name)
                                .AsNoTracking()
                                .Select(i => new TagInputViewModel
                                {
                                    TagId = i.Id,
                                    Name = i.Name,
                                    Selected = false
                                })
                                .ToListAsync()
            };

            return vm;
        }
        public async Task<CreateIngredientViewModel> GetCreateIngredientViewModel(int id)
        {
            Ingredient? ing = await context.Ingredients
                .Include(i => i.Tags)
                .Include(i => i.NutrientProfile)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (ing is null)
            {
                throw new Exception($"Ingredient with id {id} not found.");
            }
            CreateIngredientViewModel vm = new CreateIngredientViewModel
            {
                Id = id,
                Name = ing.Name,
                Notes = ing.Notes,
                NutrientProfile = new NutrientProfileViewModel
                {
                    Carbohydrates = ing.NutrientProfile.Carbohydrates,
                    Fat = ing.NutrientProfile.Fat,
                    Protein = ing.NutrientProfile.Protein
                },
                TagInputs = await context.Tags.
                                OrderBy(t => t.Name)
                                .AsNoTracking()
                                .Select(i => new TagInputViewModel
                                {
                                    TagId = i.Id,
                                    Name = i.Name,
                                    Selected = false
                                })
                                .ToListAsync()
            };

            ing.Tags
                .ToList()
                .ForEach(t =>
                {
                    var tagInput = vm.TagInputs.FirstOrDefault(ti => ti.TagId == t.Id);
                    if (tagInput != null)
                    {
                        tagInput.Selected = true;
                    }
                });
            return vm;
        }
        public async Task<TagViewModel> GetCreateTagViewModel()
        {
            return new TagViewModel();
        }
        public async Task<TagViewModel> GetCreateTagViewModel(int id)
        {
            TagViewModel vm = await context.Tags
                .Where(t => t.Id == id)
                .AsNoTracking()
                .Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl
                })
                .FirstAsync();
            return vm;
        }
        public async Task AddMealAsync(CreateMealViewModel vm)
        {
            if (context.Meals.Any(m => m.Id == vm.Id))
            {
                
                Meal meal = context.Meals.Include(m => m.Tags).Include(m => m.Ingredients).First(m => m.Id == vm.Id);
                context.Meals.Update(meal);
                meal.Name = vm.Name;
                meal.Notes = vm.Notes;


                meal.Tags.Clear();
                if (vm.TagInputs is not null) meal.Tags = context.Tags.ToList().Where(t => vm.TagInputs
                                                                    .Where(ti => ti.Selected == true)
                                                                    .Select(ti => ti.TagId)
                                                                    .Any(ti => ti == t.Id))
                                                                 .ToList();


                meal.Ingredients.Clear();
                if (vm.IngredientInputs is not null) meal.Ingredients = context.Ingredients.ToList().Where(t => vm.IngredientInputs
                                                                    .Where(ti => ti.Selected == true)
                                                                    .Select(ti => ti.IngredientId)
                                                                    .Any(ti => ti == t.Id))
                                                                    .Select(i => new MealIngredient
                                                                    {
                                                                        MealId = vm.Id,
                                                                        IngredientId = i.Id,
                                                                        Quantity = vm.IngredientInputs.First(ii => ii.IngredientId == i.Id).Quantity
                                                                    })
                                                                    .ToList();
                
                await context.SaveChangesAsync();
                return;
            }
            else
            {
                Meal meal = new Meal
                {
                    Name = vm.Name,
                    Notes = vm.Notes
                };
                if (vm.TagInputs is not null) meal.Tags = context.Tags.ToList().Where(t => vm.TagInputs
                                                                        .Where(ti => ti.Selected == true)
                                                                        .Select(ti => ti.TagId)
                                                                        .Any(ti => ti == t.Id))
                                                                     .ToList();

                if (vm.IngredientInputs is not null) meal.Ingredients = context.Ingredients.ToList().Where(t => vm.IngredientInputs
                                                                        .Where(ti => ti.Selected == true)
                                                                        .Select(ti => ti.IngredientId)
                                                                        .Any(ti => ti == t.Id))
                                                                        .Select(i => new MealIngredient
                                                                        {
                                                                            MealId = vm.Id,
                                                                            IngredientId = i.Id,
                                                                            Quantity = vm.IngredientInputs.First(ii => ii.IngredientId == i.Id).Quantity
                                                                        })
                                                                        .ToList();

                context.Meals.Add(meal);
                await context.SaveChangesAsync();
            }
        }
        public async Task AddIngredientAsync(CreateIngredientViewModel vm)
        {
            if (context.Ingredients.Any(i => i.Id == vm.Id))
            {
                Ingredient ing = context.Ingredients.Include(i => i.Tags).Include(i => i.NutrientProfile).First(i => i.Id == vm.Id);
                ing.Name = vm.Name;
                ing.Notes = vm.Notes;
                ing.NutrientProfile.Carbohydrates = vm.NutrientProfile.Carbohydrates;
                ing.NutrientProfile.Fat = vm.NutrientProfile.Fat;
                ing.NutrientProfile.Protein = vm.NutrientProfile.Protein;
                ing.Tags.Clear();
                if (vm.TagInputs is not null) ing.Tags = context.Tags.ToList().Where(t => vm.TagInputs
                                                                    .Where(ti => ti.Selected == true)
                                                                    .Select(ti => ti.TagId)
                                                                    .Any(ti => ti == t.Id))
                                                                 .ToList();
                context.Ingredients.Update(ing);
                await context.SaveChangesAsync();
                return;
            }
            else { 
                var ing = new Ingredient
                {
                    Name = vm.Name,
                    Notes = vm.Notes,
                    NutrientProfile = new NutrientProfile
                    {
                        Carbohydrates = vm.NutrientProfile.Carbohydrates,
                        Fat = vm.NutrientProfile.Fat,
                        Protein = vm.NutrientProfile.Protein
                    }
                };
            if (vm.TagInputs is not null) ing.Tags = context.Tags.ToList().Where(t => vm.TagInputs
                                                                    .Where(ti => ti.Selected == true)
                                                                    .Select(ti => ti.TagId)
                                                                    .Any(ti => ti ==t.Id))
                                                                 .ToList();
            context.Ingredients.Add(ing);
            await context.SaveChangesAsync();
            }
        }
        public async Task AddTagAsync(TagViewModel vm)
        {
            var tag = new Tag
            {
                Name = vm.Name,
                ImageUrl = vm.ImageUrl
            };

            context.Tags.Add(tag);
            await context.SaveChangesAsync();
        }
        public async Task DeleteMealAsync(int id)
        {
            Meal? meal = context.Meals.Find(id);
            if (meal != null)
            {
                context.Meals.Remove(meal);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteIngredientAsync(int id)
        {
            Ingredient? ing = context.Ingredients.Find(id);
            if (ing != null)
            {
                context.Ingredients.Remove(ing);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteTagAsync(int id)
        {
            Tag? tag = context.Tags.Find(id);
            if (tag != null)
            {
                context.Tags.Remove(tag);
                await context.SaveChangesAsync();
            }
        }
    }
}
