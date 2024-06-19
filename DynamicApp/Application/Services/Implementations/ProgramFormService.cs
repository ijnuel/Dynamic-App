using Application.Services.Abstractions;
using Application.Services.Implementations.Base;
using AutoMapper;
using Core.EntityModels;
using Core.EntityModels.Base;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
