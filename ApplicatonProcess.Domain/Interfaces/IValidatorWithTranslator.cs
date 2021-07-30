using FluentValidation;


namespace ApplicatonProcess.Domain.Interfaces
{
    public interface IValidatorWithTranslator<T> : IValidator<T>
    {
        IValidatorWithTranslator<T> Init(string language);
    }
}
