using FluentValidation;
using ApplicatonProcess.Domain.Interfaces;
using Microsoft.Extensions.Localization;

namespace ApplicatonProcess.Domain.Models.Validators
{
    public class UserValidator: AbstractValidatorWithTranslator<UserRequest>
    {
        public UserValidator(IJsonStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            
        }

        public override UserValidator Init(string language)
        {
            RuleFor(item => item.FirstName).NotNull().WithMessage(_stringLocalizer[language,"notNull", _stringLocalizer[language, "firstname"]].Value);
            RuleFor(item => item.FirstName).MinimumLength(3).WithMessage(_stringLocalizer[language, "minLength", _stringLocalizer[language, "firstname"]].Value);
            RuleFor(item => item.LastName).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "lastname"]].Value);
            RuleFor(item => item.FirstName).MinimumLength(3).WithMessage(_stringLocalizer[language, "minLength", _stringLocalizer[language, "lastname"]].Value);
            RuleFor(item => item.Age).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "age"]].Value);
            RuleFor(item => item.Age).GreaterThan(18).WithMessage(_stringLocalizer[language, "greater18", _stringLocalizer[language, "age"]].Value);
            RuleFor(item => item.Email).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "email"]].Value);
            RuleFor(item => item.Email).Matches(@".+@{1}\w+\.{1}\w{2,}").WithMessage(_stringLocalizer[language, "email_form"].Value);
            RuleFor(item => item.Address).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "address"]].Value);
            RuleFor(item => item.Address).Matches(@".+\d+\s*\,*\s*\d{5}.*").WithMessage(_stringLocalizer[language, "address_form"].Value);
            return this;
        }
    }
}
