using Core.DbContext;
using Repositories.Abstractions;
using Repositories.Implementations.Base;

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
