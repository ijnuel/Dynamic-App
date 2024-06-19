using Application.Mapper;
using Core.EntityInterfaces;
using Core.EntityModels;
using FluentValidation;

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
