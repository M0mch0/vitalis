using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Repository.Contracts;

namespace Vitalis.Data.Repository
{
    public abstract class BaseRepository  
    {
        private readonly VitalisDbContext context;

        protected BaseRepository(VitalisDbContext context)
        {
            this.context = context;
        }

        protected VitalisDbContext Context => context;
    }
}
