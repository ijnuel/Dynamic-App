using Application.Mapper;
using Application.Models.Base;
using Application.Services.Abstractions;
using Application.Services.Abstractions.Base;
using Application.Services.Implementations.Base;
using Core.EntityInterfaces;
using Core.EntityModels;
using FluentValidation;

namespace Application.Models.Dtos
{
    public class FormResponsesDto : IFormResponsesSchema, IMapFrom<FormResponses>
    {
        public Guid ProgramFormId { get; set; }
        public List<QuestionAnswerDto> QuestionAnswers { get; set; }
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
        public FormResponsesValidator(IBaseService baseService)
        {
            List<string> errorMessages = new();
            RuleFor(x => x.ProgramFormId).NotNull().NotEmpty();
            RuleFor(x => x).Must(formResponse =>
            {
                if (!baseService.Exists<ProgramForm>(x => x.Id == formResponse.ProgramFormId).Result)
                {
                    errorMessages.Add($"Invalid Program Form Id");
                }

                if (!formResponse.QuestionAnswers?.Any() ?? false)
                {
                    errorMessages.Add($"Please add responses");
                }
                QuestionAnswerValidator validationRules = new QuestionAnswerValidator();
                foreach (var questionAnswers in formResponse.QuestionAnswers ?? new())
                {
                    var validationResult = validationRules.Validate(questionAnswers);
                    if (!validationResult.IsValid)
                    {
                        errorMessages.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
                    }
                }

                return !errorMessages.Any();
            }).WithMessage(x => string.Join(" | ", errorMessages));
        }
    }

    public class FormResponsesCreateValidator : AbstractValidator<FormResponsesCreateDto>
    {
        public FormResponsesCreateValidator(IBaseService baseService)
        {
            Include(new FormResponsesValidator(baseService));
        }
    }

    public class FormResponsesUpdateValidator : AbstractValidator<FormResponsesUpdateDto>
    {
        public FormResponsesUpdateValidator(IBaseService baseService)
        {
            Include(new BaseIdValidator());
            Include(new FormResponsesValidator(baseService));
        }
    }
    #endregion
}
