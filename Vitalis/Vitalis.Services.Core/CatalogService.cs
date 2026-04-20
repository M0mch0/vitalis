using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Data.Repository.Contracts;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;
namespace Vitalis.Services.Core
{
    public class CatalogService : ICatalogService
    {
        private readonly IMealRepository mealRepository;
        private readonly ITagRepository tagRepository;
        private readonly IIngRepository ingRepository;
        public CatalogService(IMealRepository mealRepository, ITagRepository tagRepository, IIngRepository ingRepository)
        {
            this.mealRepository = mealRepository;
            this.tagRepository = tagRepository;
            this.ingRepository = ingRepository;
        }
        public async Task<(IEnumerable<MealViewModel> Meals, int TotalPages)> GetAllMealsAsync(string? searchQuery = null, int pageNumber = 1, int pageSize = 9)
        {
            IQueryable<Meal> meals = mealRepository
                .GetAllMealsAsync()
                .GetAwaiter()
                .GetResult()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                meals = meals.Where(m => m.Ingredients.Any(mi => mi.Ingredient.Name.ToLower().Contains(searchQuery)) ||
                                                m.Tags.Any(t => t.Tag.Name.ToLower().Contains(searchQuery)) ||
                                                m.Name.ToLower().Contains(searchQuery));
            }
            
            int totalMeals = meals.Count();
            int totalPages = (int)Math.Ceiling(totalMeals / (double)pageSize);

            meals = meals
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return (meals   
                .OrderBy(a => a.Name)
                .Select(m => new MealViewModel
                {
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Name = m.Name,
                    Notes = m.Notes,
                    Ingredients = m.Ingredients.Select(mi => new IngredientInputViewModel
                    {
                        IngredientId = mi.IngredientId,
                        IngredientName = mi.Ingredient.Name,
                        Quantity = mi.Quantity,
                        Selected = true,
                        NutrientProfile = new NutrientProfileViewModel
                        {
                            Carbohydrates = mi.Ingredient.NutrientProfile.Carbohydrates,
                            Fat = mi.Ingredient.NutrientProfile.Fat,
                            Protein = mi.Ingredient.NutrientProfile.Protein
                        }
                    }).ToList(),
                    Tags = tagRepository.GetAllTagsAsync().GetAwaiter().GetResult().Select(t => new TagInputViewModel
                    {
                        TagId = t.Id,
                        Name = t.Name,
                        Selected = m.Tags.Select(tm => tm.TagId).Any(tm => tm == t.Id)
                    }).ToList()
                })
                .ToList(), totalPages)  ;
                
        }
        public async Task<(IEnumerable<IngredientViewModel> Ingredients, int TotalPages)> GetAllIngredientsAsync(string? searchQuery = null, int pageNumber = 1, int pageSize = 9)
        {
            IQueryable<Ingredient> ingredients = ingRepository
                .GetAllIngredientsAsync()
                .GetAwaiter()
                .GetResult()
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();
                ingredients = ingredients.Where(m => m.Tags.Any(t => t.Tag.Name.ToLower().Contains(searchQuery)) ||
                                                     m.Name.ToLower().Contains(searchQuery)).AsQueryable();
            }

            int totalIngredients = ingredients.Count();
            int TotalPages = (int)Math.Ceiling(totalIngredients / (double)pageSize);

            ingredients = ingredients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return (ingredients
                .OrderBy(a => a.Name)
                .Select(i => new IngredientViewModel
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    Name = i.Name,
                    Tags = i.Tags.Select(t => new TagInputViewModel
                    {
                        TagId = t.TagId,
                        Selected = true,
                        Name = t.Tag.Name
                    }),
                    NutrientProfile = new NutrientProfileViewModel
                    {
                        Carbohydrates = i.NutrientProfile.Carbohydrates,
                        Fat = i.NutrientProfile.Fat,
                        Protein = i.NutrientProfile.Protein
                    }
                })
                .ToList(), TotalPages);
                
        }
        public async Task<IEnumerable<TagViewModel>> GetAllTagsAsync()
        {
            IEnumerable<TagViewModel> tags = tagRepository
                .GetAllTagsAsync()
                .GetAwaiter()
                .GetResult()
                .OrderBy(a => a.Name)
                .Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    ImageUrl = t.ImageUrl
                })
                .ToList();
            return tags;
        }
        public async Task<ViewTagViewModel> GetViewByTagAsync( int id)
        {

            Tag? tag = await tagRepository.GetByIdAsync(id);

            if(tag is null)
            { 
                return null;
            }
            var a = ingRepository.GetAllIngredientsAsync()
                    .GetAwaiter()
                    .GetResult();
            ViewTagViewModel vm = new ViewTagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Ingredients = ingRepository.GetAllIngredientsAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Where(i => i.Tags.Any(t => t.TagId == tag.Id))
                    .Select(i => new IngredientViewModel
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        Name = i.Name,
                        NutrientProfile = new NutrientProfileViewModel
                        {
                            Carbohydrates = i.NutrientProfile.Carbohydrates,
                            Fat = i.NutrientProfile.Fat,
                            Protein = i.NutrientProfile.Protein
                        },
                        Tags = i.Tags.Select(t => new TagInputViewModel
                        {
                            TagId = t.TagId,
                            Name = t.Tag.Name,
                            Selected = true
                        })
                    })
                    .ToList(),
                Meals =  mealRepository.GetAllMealsAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Where(m => m.Tags.Any(t => t.TagId == tag.Id))
                    .Select(m => new MealViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ImageUrl= m.ImageUrl,
                        Notes = m.Notes,
                        Ingredients = m.Ingredients.Select(mi => new IngredientInputViewModel
                        {
                            IngredientId = mi.IngredientId,
                            IngredientName = mi.Ingredient.Name,
                            Quantity = mi.Quantity,
                            Selected = true,
                            NutrientProfile = new NutrientProfileViewModel
                            {
                                Carbohydrates = mi.Ingredient.NutrientProfile.Carbohydrates,
                                Fat = mi.Ingredient.NutrientProfile.Fat,
                                Protein = mi.Ingredient.NutrientProfile.Protein
                            }
                        }).ToList(),
                        Tags = m.Tags.Select(t => new TagInputViewModel
                        {
                            TagId = t.TagId,
                            Name = t.Tag.Name,
                            Selected = true
                        })

                    }).ToList()
            };

            return vm;
        }
        
        public async Task<MealViewModel> GetMealByIdAsync(int id)
        {
            Meal? meal = await mealRepository.GetByIdAsync(id);

            if (meal is null)
            {
                return null;
            }

            MealViewModel vm = new MealViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                Notes = meal.Notes,
                ImageUrl = meal.ImageUrl,
                Ingredients = meal.Ingredients.Select(mi => new IngredientInputViewModel
                {
                    IngredientId = mi.IngredientId,
                    IngredientName = mi.Ingredient.Name,
                    Quantity = mi.Quantity,
                    Selected = true,
                    NutrientProfile = new NutrientProfileViewModel
                    {
                        Carbohydrates = mi.Ingredient.NutrientProfile.Carbohydrates,
                        Fat = mi.Ingredient.NutrientProfile.Fat,
                        Protein = mi.Ingredient.NutrientProfile.Protein
                    }
                }).ToList(),
                Tags = meal.Tags.Select(t => new TagInputViewModel
                {
                    TagId = t.TagId,
                    Name = t.Tag.Name,
                    Selected = true
                }).ToList()
            };
            return vm;
        }

        public async Task<IngredientViewModel> GetIngredientByIdAsync(int id)
        {
            Ingredient? ing = await ingRepository.GetByIdAsync(id);


            if (ing is null)
            {
                return null;
            }
            IngredientViewModel vm = new IngredientViewModel
            {
                Id = ing.Id,
                Name = ing.Name,
                Notes = ing.Notes, 
                ImageUrl = ing.ImageUrl,
                NutrientProfile = new NutrientProfileViewModel
                {
                    Carbohydrates = ing.NutrientProfile.Carbohydrates,
                    Fat = ing.NutrientProfile.Fat,
                    Protein = ing.NutrientProfile.Protein
                },
                Tags = ing.Tags.Select(t => new TagInputViewModel
                {
                    TagId = t.TagId,
                    Name = t.Tag.Name,
                    Selected = true
                }).ToList()
            };
            return vm;
        }

        
        public async Task DeleteMealAsync(int id)
        {
            await mealRepository.DeleteAsync(id);
        }
        public async Task DeleteIngredientAsync(int id)
        {
            await ingRepository.DeleteAsync(id);
        }
        public async Task DeleteTagAsync(int id)
        {
            await tagRepository.DeleteAsync(id);
        }
    }
}
