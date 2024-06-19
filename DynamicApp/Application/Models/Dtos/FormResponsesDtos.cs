using Application.Mapper;
using Application.Models.Base;
using Core.EntityInterfaces;
using Core.EntityModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class FormResponsesDto : IFormResponsesSchema, IMapFrom<FormResponses>
    {
        public Guid ProgramFormId { get; set; }
        public List<QuestionAnswerResponseDto> QuestionAnswers { get; set; }
    }
    public class FormResponsesCreateDto : FormResponsesDto
    {
    }
    public class FormResponsesResponseDto : FormResponsesDto, IBaseId
    {
        public Guid Id { get; set; }
    }
    public class FormResponsesUpdateDto : FormResponsesDto, IBaseId
    {
        public Guid Id { get; set; }
    }



    #region Validators
    public class FormResponsesValidator : AbstractValidator<FormResponsesDto>
    {
        public FormResponsesValidator()
        {
        }
    }

    public class FormResponsesCreateValidator : AbstractValidator<FormResponsesCreateDto>
    {
        public FormResponsesCreateValidator()
        {
            Include(new FormResponsesValidator());
        }
    }

    public class FormResponsesUpdateValidator : AbstractValidator<FormResponsesUpdateDto>
    {
        public FormResponsesUpdateValidator()
        {
            Include(new BaseIdValidator());
            Include(new FormResponsesValidator());
        }
    }
    #endregion
}
