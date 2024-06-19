using Core.DbContext;
using Repositories.Abstractions;
using Repositories.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class ProgramFormRepository : BaseRepository, IProgramFormRepository
    {
        private readonly IApplicationDBContext _dbContext;
        public ProgramFormRepository(IApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
