using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;
using Vitalis.Data.Repository.Contracts;

namespace Vitalis.Data.Repository
{
    public class JournalRepository : BaseRepository, IJournalRepository
    {
        public JournalRepository(VitalisDbContext context) : base(context)
        {
        }

        public async Task<JournalEntry> GetJournalEntryAsync(Guid userId)
        {
            return await Context.JournalEntries
                .Include(j => j.Meals)
                .ThenInclude(m => m.Meal)
                .ThenInclude(mm => mm.Ingredients)
                .Include(j => j.Ingredients)
                .FirstAsync(je => je.UserId == userId);
        }

        public async Task AddJournalEntryMealAsync(JournalEntryMeal jem)
        {
            JournalEntry journalEntry = await Context.JournalEntries.FirstAsync(je => je.Id == jem.JournalEntryId);
            journalEntry.Meals.Add(jem);
            Context.JournalEntryMeals.Add(jem);
            await Context.SaveChangesAsync();
        }

        public async Task AddJournalEntryIngredientAsync(JournalEntry je, int ingId)
        {
            var ing = Context.Ingredients.First(i => i.Id == ingId);
            Context.JournalEntryIngredients.Add(new JournalEntryIngredient
            {
                IngredientId = ing.Id,
                JournalEntryId = je.Id,
                JournalEntry = je,
                Ingredient = ing,
                Quantity = 100
            });
            await Context.SaveChangesAsync();
        }

        public async Task DeleteJournalEntryMealAsync(JournalEntryMeal jem)
        {
            JournalEntryMeal existingJem = await Context
                .JournalEntryMeals
                .FirstOrDefaultAsync(j => j.JournalEntryId == jem.JournalEntryId && j.MealId == jem.MealId);
            if (existingJem != null)
            {
                Context.JournalEntryMeals.Remove(existingJem);
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteJournalEntryIngredientAsync(JournalEntryIngredient jei)
        {
            JournalEntryIngredient existingJei = await Context
                .JournalEntryIngredients
                .FirstOrDefaultAsync(j => j.JournalEntryId == jei.JournalEntryId && j.IngredientId == jei.IngredientId);
            if (existingJei != null)
            {
                Context.JournalEntryIngredients.Remove(existingJei);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateMealAmountAsync(JournalEntryMeal jem)
        {
            JournalEntryMeal existingJem = await Context
                .JournalEntryMeals
                .FirstOrDefaultAsync(j => j.JournalEntryId == jem.JournalEntryId && j.MealId == jem.MealId);
            if (existingJem != null)
            {
                existingJem.Amount = jem.Amount;
                Context.JournalEntryMeals.Update(existingJem);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateIngredientQuantityAsync(JournalEntryIngredient jei)
        {
            JournalEntryIngredient existingJei = await Context
                .JournalEntryIngredients
                .FirstOrDefaultAsync(j => j.JournalEntryId == jei.JournalEntryId && j.IngredientId == jei.IngredientId);
            if (existingJei != null)
            {
                existingJei.Quantity = jei.Quantity;
                Context.JournalEntryIngredients.Update(existingJei);
                await Context.SaveChangesAsync();
            }
        }
    }
}
