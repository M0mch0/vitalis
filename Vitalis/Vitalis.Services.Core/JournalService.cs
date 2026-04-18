using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data;
using Vitalis.Data.Models;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core
{
    public class JournalService : IJournalService
    {
        private readonly VitalisDbContext context;

        public JournalService(VitalisDbContext context)
        {
            this.context = context;
        }
        public async Task<JournalEntryViewModel> GetJournalEntryAsync(string userId)
        {
            var journal = await context.JournalEntries
                .Include(j => j.Meals)
                .ThenInclude(m =>m.Meal)
                .ThenInclude(mm => mm.Ingredients)
                .Include(j => j.Ingredients)
                .FirstAsync(je => je.UserId == userId);

            var journalIngredientMap = journal.Ingredients
                .ToDictionary(ji => ji.IngredientId, ji => ji.Quantity);

            var ingredients = await context.Ingredients
                .Include(i => i.NutrientProfile)
                .Select(i => new
                {
                    i.Id,
                    i.Name,
                    Carbs = i.NutrientProfile.Carbohydrates,
                    Protein = i.NutrientProfile.Protein,
                    Fats = i.NutrientProfile.Fat
                })
                .ToListAsync();

            JournalEntryViewModel journalvm = new JournalEntryViewModel
            {
                UserId = userId,
                Meals = context.Meals.Select(m => new MealInputViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Selected = journal.Meals.Select(tm => tm.MealId).Any(tm => tm == m.Id),
                    Ingredients = context.Ingredients.Select(i=> new JournalIngredientViewModel
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
                Ingredients = ingredients.Select(i => new JournalIngredientViewModel
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
                    // Find the matching JournalEntryMeal for this meal (if any)
                    var jm = journal.Meals.FirstOrDefault(m => m.MealId == meal.Id);
                    if (jm == null) continue; // not in the journal -> skip

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

        public async Task AddToJournalAsync(string userId, JournalEntryViewModel input)
        {
              var journal = await context.JournalEntries
                .Include(j => j.Meals)
                .Include(j => j.Ingredients)
                .FirstAsync(je => je.UserId == userId);
            if(input.Meals is not null)
            {
                foreach (var meal in input.Meals.Where(m => m.Selected))
                {
                    var mealEntity = await context.Meals.FindAsync(meal.Id);
    
                    if (mealEntity == null) continue;
                    var jem = new JournalEntryMeal
                                 {
                                     MealId = meal.Id,
                                     JournalEntryId = journal.Id,
                                     JournalEntry = journal,
                                     Meal = mealEntity,
                                     Amount = 1
                                  };
    
                    journal.Meals.Add(jem);
                    context.JournalEntryMeals.Add(jem); 
                    
                    
                }
            }
            if (input.Ingredients is not null)
            {
                foreach (var ingredient in input.Ingredients.Where(i => i.Selected))
                {
                    var ingEntity = await context.Ingredients.FindAsync(ingredient.IngredientId);
                    if (ingEntity == null) continue;
                    var jei = new JournalEntryIngredient
                    {
                        IngredientId = ingredient.IngredientId,
                        JournalEntryId = journal.Id,
                        JournalEntry = journal,
                        Ingredient = ingEntity,
                        Quantity = ingredient.Quantity
                    };

                    journal.Ingredients.Add(jei);
                    context.JournalEntryIngredients.Add(jei);
                }
            }
            await context.SaveChangesAsync();
            return;
        }

        public async Task RemoveFromJournalAsync(string userId, int id, bool MealOrIng)
        {
            var journal = await context.JournalEntries
                .Include(j => j.Meals)
                .Include(j => j.Ingredients)
                .FirstAsync(je => je.UserId == userId);
            if (MealOrIng)
            {
                var mealToRemove = journal.Meals.FirstOrDefault(m => m.MealId == id);
                if (mealToRemove != null)
                {
                    journal.Meals.Remove(mealToRemove);
                    context.JournalEntryMeals.Remove(mealToRemove);
                }
            }
            else
            {
                var ingToRemove = journal.Ingredients.FirstOrDefault(i => i.IngredientId == id);
                if (ingToRemove != null)
                {
                    journal.Ingredients.Remove(ingToRemove);
                    context.JournalEntryIngredients.Remove(ingToRemove);
                }
             }
            await context.SaveChangesAsync();
             return;
        }

        public async Task UpdateQuantityAsync(string userId, int id, double quantity)
        {
            var journal = await context.JournalEntries
                .Include(j => j.Ingredients)
                .FirstAsync(je => je.UserId == userId);
            var ingredientToUpdate = journal.Ingredients.FirstOrDefault(i => i.IngredientId == id);
            if (ingredientToUpdate != null)
            {
                ingredientToUpdate.Quantity = quantity;
                context.JournalEntryIngredients.Update(ingredientToUpdate);
                await context.SaveChangesAsync();
            }
             return;
        }
        public async Task UpdateAmountAsync(string userId, int id, int amount)
        {
            var journal = await context.JournalEntries
                .Include(j => j.Meals)
                .FirstAsync(je => je.UserId == userId);
            var mealToUpdate = journal.Meals.FirstOrDefault(i => i.MealId == id);
            if (mealToUpdate != null)
            {
                mealToUpdate.Amount = amount;
                context.JournalEntryMeals.Update(mealToUpdate);
                await context.SaveChangesAsync();
            }
            return;
        }
    }
}
