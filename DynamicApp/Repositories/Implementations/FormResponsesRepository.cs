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
    public class FormResponsesRepository : BaseRepository, IFormResponsesRepository
    {
        private readonly IApplicationDBContext _dbContext;
        public FormResponsesRepository(IApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
