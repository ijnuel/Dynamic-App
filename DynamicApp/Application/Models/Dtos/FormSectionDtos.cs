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
            List<string> errorMessages = new();
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x).Must(section =>
            {
                if (!section.Questions?.Any() ?? false)
                {
                    errorMessages.Add($"Please add questions to {section.Title}");
                }
                DynamicQuestionValidator validationRules = new DynamicQuestionValidator();
                foreach (var question in section.Questions ?? new())
                {
                    var validationResult = validationRules.Validate(question);
                    if (!validationResult.IsValid)
                    {
                        errorMessages.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
                    }
                }

                return !errorMessages.Any();
            }).WithMessage(x => string.Join(" | ", errorMessages));
        }
    }
    #endregion
}
