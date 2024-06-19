using Core.DbContext;
using Repositories.Abstractions;
using Repositories.Implementations.Base;

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
