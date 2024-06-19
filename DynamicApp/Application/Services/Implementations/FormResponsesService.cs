using Application.Services.Abstractions;
using Application.Services.Implementations.Base;
using AutoMapper;
using Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
