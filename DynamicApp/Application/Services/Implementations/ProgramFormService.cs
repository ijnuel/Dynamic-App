using Application.Services.Abstractions;
using Application.Services.Implementations.Base;
using AutoMapper;
using Repositories.Abstractions;

namespace Application.Services.Implementations
{
    public class ProgramFormService : BaseService, IProgramFormService
    {
        private readonly IProgramFormRepository _programFormRepo;
        private readonly IMapper _mapper;
        public ProgramFormService(IProgramFormRepository programFormRepo, IMapper mapper) : base(programFormRepo, mapper)
        {
            _programFormRepo = programFormRepo;
            _mapper = mapper;
        }
    }
}
