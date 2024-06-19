using Application.Models.Dtos;
using Core.SessionIdentitier;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Base
{
    public interface IIdentityBase { }
    /*public class BaseValidator : AbstractValidator<IIdentityBase>
    {
        private readonly ISessionIdentifier _sessionIdentifier;
        public BaseValidator(ISessionIdentifier sessionIdentifier)
        {
            _sessionIdentifier = sessionIdentifier;

            _sessionIdentifier.GetUserDetails();
        }
    }*/


    public interface IBaseId : IIdentityBase
    {
        public Guid Id { get; set; }
    }
    public class BaseIdValidator : AbstractValidator<IBaseId>
    {
        public BaseIdValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            //Include(new BaseValidator());
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
            //Include(new BaseValidator());
        }
    }
}
