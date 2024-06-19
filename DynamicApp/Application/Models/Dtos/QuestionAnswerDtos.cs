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
    public class QuestionAnswerDto : IQuestionAnswerSchema, IMapFrom<QuestionAnswer>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class QuestionAnswerCreateDto : QuestionAnswerDto
    {
    }
    public class QuestionAnswerResponseDto : QuestionAnswerDto
    {
    }
    public class QuestionAnswerUpdateDto : QuestionAnswerDto
    {
    }



    #region Validators
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswerDto>
    {
        public QuestionAnswerValidator()
        {
        }
    }
    #endregion
}
