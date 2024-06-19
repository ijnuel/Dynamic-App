using Application.Mapper;
using Core.EntityInterfaces;
using Core.EntityModels;
using Core.Enums;
using FluentValidation;

namespace Application.Models.Dtos
{
    public class DynamicQuestionDto : IDynamicQuestionSchema, IMapFrom<DynamicQuestion>
    {
        public QuestionType Type { get; set; }
        public string Question { get; set; }
        private List<string> _choices;
        public List<string> Choices { 
            get
            {
                return _choices;
            } 
            set
            {
                _choices = Type == QuestionType.YesNo || Type == QuestionType.MultipleChoice ? value : new();
            }
        }
        private bool _isOtherOptionEnabled { get; set; }
        public bool IsOtherOptionEnabled
        {
            get
            {
                return _isOtherOptionEnabled;
            }
            set
            {
                _isOtherOptionEnabled = Type == QuestionType.YesNo || Type == QuestionType.MultipleChoice ? value : false;
            }
        }
        private int _maxChoiceAllowed { get; set; }
        public int MaxChoiceAllowed
        {
            get
            {
                return _maxChoiceAllowed;
            }
            set
            {
                _maxChoiceAllowed = Type == QuestionType.MultipleChoice ? value : 0;
            }
        }
        public bool IsMandatory { get; set; }
        public bool IsInternal { get; set; }
        public bool IsHidden { get; set; }
    }



    #region Validators
    public class DynamicQuestionValidator : AbstractValidator<DynamicQuestionDto>
    {
        public DynamicQuestionValidator()
        {
            List<string> errorMessages = new();
            RuleFor(x => x.Question).NotNull().NotEmpty();
            RuleFor(x => x).Must(question =>
            {
                if (question.Type == QuestionType.Dropdown || question.Type == QuestionType.MultipleChoice)
                {
                    if (!question.Choices.Any())
                    {
                        errorMessages.Add($"Please include choices for '{question.Question}'");
                    }
                }
                if (question.Type == QuestionType.MultipleChoice && question.MaxChoiceAllowed <= 0)
                {
                    errorMessages.Add($"Max choice allowed for '{question.Question}' must be greater than zero");
                }

                return !errorMessages.Any();
            }).WithMessage(x => string.Join(" | ", errorMessages));
        }
    }
    #endregion
}
