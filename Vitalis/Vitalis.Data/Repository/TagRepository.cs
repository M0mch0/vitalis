using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;
using Vitalis.Data.Repository.Contracts;

namespace Vitalis.Data.Repository
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(VitalisDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await Context
                .Tags
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await Context
                .Tags
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Tag tag)
        {
            await Context
                .Tags
                .AddAsync(tag);
            await Context
                .SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await GetByIdAsync(id);
            if (tag != null)
            {
                Context
                    .Tags
                    .Remove(tag);
                await Context
                    .SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Tag tag)
        {
            var existingTag = await GetByIdAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.ImageUrl = tag.ImageUrl;
                Context.Tags.Update(existingTag);
                await Context.SaveChangesAsync();
            }
            
        }
    }
}
