using Application.Mapper;
using Core.EntityInterfaces;
using Core.EntityModels;
using FluentValidation;

namespace Application.Models.Dtos
{
    public class FormSectionDto : IFormSectionSchema, IMapFrom<FormSection>
    {
        public string Title { get; set; }
        public List<DynamicQuestionDto> Questions { get; set; }
    }



    #region Validators
    public class FormSectionValidator : AbstractValidator<FormSectionDto>
    {
        public FormSectionValidator()
        {
            var message = "An Error Occured!";
            RuleFor(x => x).Must(section =>
            {
                if (!section.Questions?.Any() ?? false)
                {
                    message = $"Please add questions to {section.Title}";
                    return false;
                }
                DynamicQuestionValidator validationRules = new DynamicQuestionValidator();
                foreach (var question in section.Questions)
                {
                    var validationResult = validationRules.Validate(question);
                    if (!validationResult.IsValid)
                    {
                        message = string.Join("||", validationResult.Errors.Select(x => x.ErrorMessage));
                        return false;
                    }
                }
                return true;
            }).WithMessage(message);
        }
    }
    #endregion
}
