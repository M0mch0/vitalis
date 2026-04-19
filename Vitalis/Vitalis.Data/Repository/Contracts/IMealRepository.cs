using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;

namespace Vitalis.Data.Repository.Contracts
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal> GetByIdAsync(int id);

        Task AddAsync(Meal meal);

        Task UpdateAsync(Meal meal);

        Task DeleteAsync(int id);



    }
}
