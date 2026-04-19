using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;

namespace Vitalis.Data.Repository.Contracts
{
    public interface IJournalRepository
    {
        Task<JournalEntry> GetJournalEntryAsync(Guid userId);
        Task AddJournalEntryMealAsync(JournalEntryMeal jem);

        Task AddJournalEntryIngredientAsync(JournalEntry je, int ingId);

        Task DeleteJournalEntryMealAsync(JournalEntryMeal jem);

        Task DeleteJournalEntryIngredientAsync(JournalEntryIngredient jei);

        Task UpdateIngredientQuantityAsync(JournalEntryIngredient jei);

        Task UpdateMealAmountAsync(JournalEntryMeal jem);
    }
}
