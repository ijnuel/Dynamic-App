using Application.Mapper;
using Core.EntityInterfaces;
using Core.EntityModels;
using Core.Enums;
using FluentValidation;
using System.Globalization;
using System.Linq;

namespace Application.Models.Dtos
{
    public class QuestionAnswerDto : IQuestionAnswerSchema, IMapFrom<QuestionAnswer>
    {
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsMandatory { get; set; }
        public string Answer { get; set; }
    }



    #region Validators
    public class QuestionAnswerValidator : AbstractValidator<QuestionAnswerDto>
    {
        public QuestionAnswerValidator()
        {
            List<string> errorMessages = new();
            RuleFor(x => x.Question).NotNull().NotEmpty();
            RuleFor(x => x).Must(questionAnswer =>
            {
                bool answerIsEmpty = string.IsNullOrWhiteSpace(questionAnswer.Answer);
                if (questionAnswer.IsMandatory && answerIsEmpty)
                {
                    errorMessages.Add($"Invalid response for '{questionAnswer.Question}'");
                }
                if (!answerIsEmpty && questionAnswer.QuestionType == QuestionType.Date && !DateTime.TryParse(questionAnswer.Answer, out DateTime date))
                {
                    errorMessages.Add($"Date is required for '{questionAnswer.Question}'");
                }
                if (!answerIsEmpty && questionAnswer.QuestionType == QuestionType.Number && !double.TryParse(questionAnswer.Answer, out double value))
                {
                    errorMessages.Add($"Number is required for '{questionAnswer.Question}'");
                }

                return !errorMessages.Any();
            }).WithMessage(x => string.Join(" | ", errorMessages));
        }
    }
    #endregion
}
