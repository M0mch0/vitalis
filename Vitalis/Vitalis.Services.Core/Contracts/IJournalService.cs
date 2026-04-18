using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core.Contracts
{
    public interface IJournalService
    {
        Task<JournalEntryViewModel> GetJournalEntryAsync(string userId);

        Task AddToJournalAsync(string userId, JournalEntryViewModel vm);

        Task RemoveFromJournalAsync(string userId, int id, bool MealOrIng);

        Task UpdateQuantityAsync(string userId, int id, double quantity);

        Task UpdateAmountAsync(string userId, int id, int amount);
    }
}
