using Application.Mapper;
using Application.Models.Base;
using Core.EntityInterfaces;
using Core.EntityModels;
using FluentValidation;

namespace Application.Models.Dtos
{
    public class ProgramFormDto : IProgramFormSchema, IMapFrom<ProgramForm>
    {
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }
        public List<FormSectionDto> Sections { get; set; }
    }
    public class ProgramFormCreateDto : ProgramFormDto
    {
    }
    public class ProgramFormResponseDto : ProgramFormDto, IBaseId
    {
        public Guid Id { get; set; }
    }
    public class ProgramFormUpdateDto : ProgramFormDto, IBaseId
    {
        public Guid Id { get; set; }
    }



    #region Validators
    public class ProgramFormValidator : AbstractValidator<ProgramFormDto>
    {
        public ProgramFormValidator()
        {
            var message = $"Please add form sections!";
            RuleFor(x => x.Sections).NotNull().NotEmpty().Must(sections =>
            {
                FormSectionValidator validationRules = new FormSectionValidator();
                foreach (var section in sections)
                {
                    var validationResult = validationRules.Validate(section);
                    if (!validationResult.IsValid)
                    {
                        message = string.Join("||", validationResult.Errors.Select(x => x.ErrorMessage));
                        return false;
                    }
                }
                return true;
            }).NotEmpty().WithMessage(message);
        }
    }

    public class ProgramFormCreateValidator : AbstractValidator<ProgramFormCreateDto>
    {
        public ProgramFormCreateValidator()
        {
            Include(new ProgramFormValidator());
        }
    }

    public class ProgramFormUpdateValidator : AbstractValidator<ProgramFormUpdateDto>
    {
        public ProgramFormUpdateValidator()
        {
            Include(new BaseIdValidator());
            Include(new ProgramFormValidator());
        }
    }
    #endregion
}
