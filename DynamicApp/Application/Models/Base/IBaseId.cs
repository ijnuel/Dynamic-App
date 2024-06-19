using FluentValidation;

namespace Application.Models.Base
{
    public interface IIdentityBase { }


    public interface IBaseId : IIdentityBase
    {
        public Guid Id { get; set; }
    }
    public class BaseIdValidator : AbstractValidator<IBaseId>
    {
        public BaseIdValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }


    public interface IBaseName : IIdentityBase
    {
        public string Name { get; set; }
    }
    public class BaseNameValidator : AbstractValidator<IBaseName>
    {
        public BaseNameValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
