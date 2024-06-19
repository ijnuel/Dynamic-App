using Application.Services.Abstractions;
using Application.Services.Implementations.Base;
using AutoMapper;
using Repositories.Abstractions;

namespace Application.Services.Implementations
{
    public class FormResponsesService : BaseService, IFormResponsesService
    {
        private readonly IFormResponsesRepository _formResponsesRepo;
        private readonly IMapper _mapper;
        public FormResponsesService(IFormResponsesRepository formResponsesRepo, IMapper mapper) : base(formResponsesRepo, mapper)
        {
            _formResponsesRepo = formResponsesRepo;
            _mapper = mapper;
        }
    }
}
