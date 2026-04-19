using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;

namespace Vitalis.Data.Repository.Contracts
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetByIdAsync(int id);

        Task AddAsync(Tag tag);

        Task UpdateAsync(Tag tag);

        Task DeleteAsync(int id);
    }
}
