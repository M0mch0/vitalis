using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Data.Repository.Contracts;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core
{
    public class CreateService : ICreateService
    {
        private readonly IMealRepository mealRepository;
        private readonly ITagRepository tagRepository;
        private readonly IIngRepository ingRepository;
        public CreateService(IMealRepository mealRepository, ITagRepository tagRepository, IIngRepository ingRepository)
        {
            this.mealRepository = mealRepository;
            this.tagRepository = tagRepository;
            this.ingRepository = ingRepository;
        }
        public async Task<CreateMealViewModel> GetCreateMealViewModel()
        {
            CreateMealViewModel vm = new CreateMealViewModel
            {
                IngredientInputs = ingRepository.GetAllIngredientsAsync()
                                .GetAwaiter()
                                .GetResult()
                                .OrderBy(i => i.Name)
                                .Select(i => new IngredientInputViewModel
                                {
                                    IngredientId = i.Id,
                                    IngredientName = i.Name,
                                    Selected = false,
                                    Quantity = 0,
                                    NutrientProfile = new NutrientProfileViewModel
                                    {
                                        Carbohydrates = i.NutrientProfile.Carbohydrates,
                                        Fat = i.NutrientProfile.Fat,
                                        Protein = i.NutrientProfile.Protein
                                    }
                                })
                                .ToList(),
                TagInputs = tagRepository.GetAllTagsAsync()
                                .GetAwaiter()
                                .GetResult()
                                .OrderBy(t => t.Name)
                                .Select(i => new TagInputViewModel
                                {
                                    TagId = i.Id,
                                    Name = i.Name,
                                    Selected = false
                                })
                                .ToList()
            };

            return vm;
        }
        public async Task<CreateMealViewModel> GetCreateMealViewModel(int id)
        {
            Meal? meal = mealRepository.
                GetByIdAsync(id)
                .GetAwaiter()
                .GetResult();

            if (meal is null)
            {
                throw new Exception($"Meal with id {id} not found.");
            }
            CreateMealViewModel vm = new CreateMealViewModel
            {
                Id = id,
                Name = meal.Name,
                Notes = meal.Notes,
                ImageUrl = meal.ImageUrl,
                IngredientInputs = ingRepository.GetAllIngredientsAsync()
                                .GetAwaiter()
                                .GetResult()
                                .OrderBy(i => i.Name)
                                .Select(i => new IngredientInputViewModel
                                {
                                    IngredientId = i.Id,
                                    IngredientName = i.Name,
                                    Selected = false,
                                    Quantity = 0,
                                    NutrientProfile = new NutrientProfileViewModel
                                    {
                                        Carbohydrates = i.NutrientProfile.Carbohydrates,
                                        Fat = i.NutrientProfile.Fat,
                                        Protein = i.NutrientProfile.Protein
                                    }
                                })
                                .ToList(),
                TagInputs = tagRepository.GetAllTagsAsync()
                                .GetAwaiter()   
                                .GetResult()
                                .OrderBy(t => t.Name)
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
                        ingredientInput.IngredientName = ingRepository.GetByIdAsync(mi.IngredientId).GetAwaiter().GetResult().Name;
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
                        tagInput.Name = tagRepository.GetByIdAsync(t.Id).GetAwaiter().GetResult().Name;
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
                TagInputs = tagRepository.GetAllTagsAsync()
                                .GetAwaiter()
                                .GetResult()    
                                .OrderBy(t => t.Name)
                                .Select(i => new TagInputViewModel
                                {
                                    TagId = i.Id,
                                    Name = i.Name,
                                    Selected = false
                                })
                                .ToList()
            };

            return vm;
        }
        public async Task<CreateIngredientViewModel> GetCreateIngredientViewModel(int id)
        {
            Ingredient? ing = ingRepository
                .GetByIdAsync(id)
                .GetAwaiter()
                .GetResult();

            if (ing is null)
            {
                throw new Exception($"Ingredient with id {id} not found.");
            }
            CreateIngredientViewModel vm = new CreateIngredientViewModel
            {
                Id = id,
                Name = ing.Name,
                Notes = ing.Notes,
                ImageUrl = ing.ImageUrl,
                NutrientProfile = new NutrientProfileViewModel
                {
                    Carbohydrates = ing.NutrientProfile.Carbohydrates,
                    Fat = ing.NutrientProfile.Fat,
                    Protein = ing.NutrientProfile.Protein
                },
                TagInputs = tagRepository.GetAllTagsAsync()
                                .GetAwaiter()
                                .GetResult()
                                .OrderBy(t => t.Name)
                                .Select(i => new TagInputViewModel
                                {
                                    TagId = i.Id,
                                    Name = i.Name,
                                    Selected = false
                                })
                                .ToList()
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
            Tag t = tagRepository.GetByIdAsync(id)
                    .GetAwaiter()
                    .GetResult();

            TagViewModel vm = new TagViewModel
            {
                Id = id,
                Name = t.Name,
                ImageUrl = t.ImageUrl
            };

            return vm;
        }
        public async Task AddMealAsync(CreateMealViewModel vm)
        {
            Meal meal = new Meal
            {
                Id = vm.Id,
                Name = vm.Name,
                ImageUrl = vm.ImageUrl,
                Notes = vm.Notes,
                Tags = tagRepository
                        .GetAllTagsAsync()
                        .GetAwaiter()
                        .GetResult()
                        .Where(t => vm.TagInputs
                            .Where(ti => ti.Selected == true)
                            .Select(ti => ti.TagId)
                            .Any(ti => ti == t.Id))
                        .ToList(),
                Ingredients = ingRepository
                        .GetAllIngredientsAsync()
                        .GetAwaiter()
                        .GetResult()
                        .Where(i => vm.IngredientInputs
                            .Where(ti => ti.Selected == true)
                            .Select(ti => ti.IngredientId)
                            .Any(ti => ti == i.Id))
                            .Select(i => new MealIngredient
                            {
                                MealId = vm.Id,
                                IngredientId = i.Id,
                                Quantity = vm.IngredientInputs.First(ii => ii.IngredientId == i.Id).Quantity
                            })
                        .ToList()

            };
            if (mealRepository.GetByIdAsync(vm.Id).GetAwaiter().GetResult() != null)
            {
                await mealRepository.UpdateAsync(meal);
            }
            else
            {
                await mealRepository.AddAsync(meal);
            }
        }
        public async Task AddIngredientAsync(CreateIngredientViewModel vm)
        {
            Ingredient ing = new Ingredient
            {
                Id = vm.Id,
                Name = vm.Name,
                Notes = vm.Notes,
                ImageUrl = vm.ImageUrl,
                NutrientProfile = new NutrientProfile
                {
                    Carbohydrates = vm.NutrientProfile.Carbohydrates,
                    Fat = vm.NutrientProfile.Fat,
                    Protein = vm.NutrientProfile.Protein
                },
                Tags = tagRepository
                        .GetAllTagsAsync()
                        .GetAwaiter()
                        .GetResult()
                        .Where(t => vm.TagInputs
                            .Where(ti => ti.Selected == true)
                            .Select(ti => ti.TagId)
                            .Any(ti => ti == t.Id))
                        .ToList()
            };

            if (ingRepository.GetByIdAsync(vm.Id).GetAwaiter().GetResult() != null)
            {
                await ingRepository.UpdateAsync(ing);
            }
            else
            {
                await ingRepository.AddAsync(ing);
            }
        }
        public async Task AddTagAsync(TagViewModel vm)
        {
            var tag = new Tag
            {
                Id = vm.Id,
                Name = vm.Name,
                ImageUrl = vm.ImageUrl
            };

            if (tagRepository.GetByIdAsync(vm.Id).GetAwaiter().GetResult() != null)
            {
                await tagRepository.UpdateAsync(tag);
            }
            else
            {
                await tagRepository.AddAsync(tag);
            }
        }
    }
}
