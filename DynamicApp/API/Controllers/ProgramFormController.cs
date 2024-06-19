using API.Controllers.Base;
using Application.Models.Dtos;
using Application.Services.Abstractions;
using Core.EntityModels;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class ProgramFormController : BaseEntityController<ProgramFormResponseDto, ProgramForm, ProgramFormCreateDto, ProgramFormUpdateDto>
    {
        private readonly IProgramFormService _programFormService;
        public ProgramFormController(IProgramFormService programFormService) : base(programFormService)
        {
            _programFormService = programFormService;
        }
    }
}
