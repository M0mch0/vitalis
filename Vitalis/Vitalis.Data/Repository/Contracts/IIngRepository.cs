using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;

namespace Vitalis.Data.Repository.Contracts
{
    public interface IIngRepository
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient> GetByIdAsync(int id);

        Task AddAsync(Ingredient ingredient);

        Task UpdateAsync(Ingredient ingredient);

        Task DeleteAsync(int id);
    }
}
