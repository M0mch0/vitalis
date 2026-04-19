using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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
    public class JournalService : IJournalService
    {
        private readonly IMealRepository mealRepository;
        private readonly IJournalRepository journalRepository;
        private readonly IIngRepository ingRepository;

        public JournalService(IMealRepository mealRepository, IJournalRepository journalRepository, IIngRepository ingRepository)
        {
            this.mealRepository = mealRepository;
            this.journalRepository = journalRepository;
            this.ingRepository = ingRepository;
        }
        public async Task<JournalEntryViewModel> GetJournalEntryAsync(string userId)
        {
            var journal = journalRepository
                .GetJournalEntryAsync(Guid.Parse(userId))
                .GetAwaiter()
                .GetResult();
            var journalIngredientMap = journal.Ingredients
                .ToDictionary(ji => ji.IngredientId, ji => ji.Quantity);

            var ingredients = ingRepository.GetAllIngredientsAsync().GetAwaiter().GetResult()
                .Select(i => new
                {
                    i.Id,
                    i.Name,
                    Carbs = i.NutrientProfile.Carbohydrates,
                    Protein = i.NutrientProfile.Protein,
                    Fats = i.NutrientProfile.Fat
                })
                .ToList();

            JournalEntryViewModel journalvm = new JournalEntryViewModel
            {
                UserId = userId,
                Meals = mealRepository
                    .GetAllMealsAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Select(m => new MealInputViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Selected = journal
                            .Meals
                            .Select(tm => tm.MealId)
                            .Any(tm => tm == m.Id),
                        Ingredients = ingRepository
                            .GetAllIngredientsAsync()
                            .GetAwaiter()
                            .GetResult()
                            .Select(i=> new JournalIngredientViewModel
                            {
                                IngredientId = i.Id,
                                Carbs = i.NutrientProfile.Carbohydrates,
                                Protein = i.NutrientProfile.Protein,
                                Fats = i.NutrientProfile.Fat,
                                IngredientName = i.Name,
                                Quantity = 0,
                                Selected = false
                            }).ToList(),
                        Amount = 0
                    }).ToList(),
                    Ingredients = ingredients
                        .Select(i => new JournalIngredientViewModel
                        {
                             IngredientId = i.Id,
                             IngredientName = i.Name,
                             Quantity = journalIngredientMap.TryGetValue(i.Id, out double q) ? q : 0,
                             Selected = journal.Ingredients.Select(tm => tm.IngredientId).Any(tm => tm == i.Id),
                             Carbs = i.Carbs,
                             Protein = i.Protein, 
                             Fats = i.Fats
                         }).ToList()
            };
            if (journal.Meals is not null)
            {
                foreach (MealInputViewModel meal in journalvm.Meals)
                {
                    var jm = journal.Meals.FirstOrDefault(m => m.MealId == meal.Id);
                    if (jm == null) continue;

                    ICollection<MealIngredient>? mi = jm.Meal?.Ingredients;
                    if (meal.Ingredients is null) continue;

                    if (mi is not null && mi.Any())
                    {
                        meal.Ingredients
                            .Where(ing => mi.Any(i => i.IngredientId == ing.IngredientId))
                            .ToList()
                            .ForEach(i => i.Selected = true);

                        meal.Ingredients
                            .Where(ing => mi.Any(i => i.IngredientId == ing.IngredientId))
                            .ToList()
                            .ForEach(i => i.Quantity = mi.First(ing => ing.IngredientId == i.IngredientId).Quantity);
                    }

                    meal.Amount = jm.Amount;
                }
            }
            
            return journalvm;
        }

        public async Task AddToJournalAsync(string userId, JournalEntryViewModel vm)
        {
            var journalEntry = await journalRepository.GetJournalEntryAsync(Guid.Parse(userId));
            if (journalEntry == null) return;

            if(vm.Meals is not null)
            {
                foreach (var meal in vm.Meals.Where(m => m.Selected))
                {
                    var mealEntity = mealRepository.GetByIdAsync(meal.Id).GetAwaiter().GetResult();
                    if (mealEntity == null) continue;
                    var jem = new JournalEntryMeal
                    { 
                        MealId = meal.Id,
                        JournalEntryId = journalEntry.Id,
                        JournalEntry = journalEntry,
                        Meal = mealEntity,
                        Amount = 1
                    };
    
                    await journalRepository.AddJournalEntryMealAsync(jem);

                }
            }
            if (vm.Ingredients is not null)
            {
                foreach (var ingredient in vm.Ingredients.Where(i => i.Selected))
                {
                    var ingEntity = ingRepository.GetByIdAsync(ingredient.IngredientId).GetAwaiter().GetResult();
                    if (ingEntity == null) continue;
                    
                    await journalRepository.AddJournalEntryIngredientAsync(journalEntry, ingEntity.Id);
                }
            }
        }

        public async Task RemoveFromJournalAsync(string userId, int id, bool MealOrIng)
        {
            var journal = journalRepository.GetJournalEntryAsync(Guid.Parse(userId)).GetAwaiter().GetResult();
            if (MealOrIng)
            {
                var mealToRemove = journal.Meals.FirstOrDefault(m => m.MealId == id);
                if (mealToRemove != null)
                {
                    await journalRepository.DeleteJournalEntryMealAsync(mealToRemove);
                }
            }
            else
            {
                var ingToRemove = journal.Ingredients.FirstOrDefault(i => i.IngredientId == id);
                if (ingToRemove != null)
                {
                    await journalRepository.DeleteJournalEntryIngredientAsync(ingToRemove);
                }
             }
        }

        public async Task UpdateQuantityAsync(string userId, int id, double quantity)
        {
            var journal = journalRepository.GetJournalEntryAsync(Guid.Parse(userId)).GetAwaiter().GetResult();
            var ingredientToUpdate = journal.Ingredients.FirstOrDefault(i => i.IngredientId == id);
            if (ingredientToUpdate != null)
            {
                ingredientToUpdate.Quantity = quantity;
                await journalRepository.UpdateIngredientQuantityAsync(ingredientToUpdate);
            }
        }
        public async Task UpdateAmountAsync(string userId, int id, int amount)
        {
            var journal = journalRepository.GetJournalEntryAsync(Guid.Parse(userId)).GetAwaiter().GetResult();
            var mealToUpdate = journal.Meals.FirstOrDefault(i => i.MealId == id);
            if (mealToUpdate != null)
            {
                mealToUpdate.Amount = amount;
                await journalRepository.UpdateMealAmountAsync(mealToUpdate);
            }
            return;
        }
    }
}
