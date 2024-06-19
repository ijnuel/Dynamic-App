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
            List<string> errorMessages = new();
            RuleFor(x => x.Sections).NotNull().NotEmpty();
            RuleFor(x => x.ProgramTitle).NotNull().NotEmpty();
            RuleFor(x => x.ProgramDescription).NotNull().NotEmpty();
            RuleFor(x => x).Must(programForm =>
            {
                FormSectionValidator validationRules = new FormSectionValidator();
                foreach (var section in programForm.Sections)
                {
                    var validationResult = validationRules.Validate(section);
                    if (!validationResult.IsValid)
                    {
                        errorMessages.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
                    }
                }

                return !errorMessages.Any();
            }).NotEmpty().WithMessage(x => string.Join(" | ", errorMessages));
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
