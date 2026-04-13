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
                    Selected = journal.Meals.Select(tm => tm.MealId).Any(tm => tm == m.Id)
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
    }
}
