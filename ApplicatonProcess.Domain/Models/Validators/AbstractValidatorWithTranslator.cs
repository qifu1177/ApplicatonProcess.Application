using FluentValidation;
using ApplicatonProcess.Domain.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Models.Validators
{
    public abstract class AbstractValidatorWithTranslator<T>: AbstractValidator<T>, IValidatorWithTranslator<T>
    {
        protected IJsonStringLocalizer _stringLocalizer;

        public abstract IValidatorWithTranslator<T> Init(string language);
        
    }
}
