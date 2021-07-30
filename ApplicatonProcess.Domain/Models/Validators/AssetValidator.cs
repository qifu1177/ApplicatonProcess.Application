using FluentValidation;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Logics;
using Microsoft.Extensions.Localization;

namespace ApplicatonProcess.Domain.Models.Validators
{
    public class AssetValidator : AbstractValidatorWithTranslator<AssetRequest>
    {
        public AssetValidator(IJsonStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;           
        }

        public override AssetValidator Init(string language)
        {
            RuleFor(item => item.Name).NotNull().WithMessage(_stringLocalizer[language,"notNull", _stringLocalizer[language,"name"]].Value);
            RuleFor(item => item.MarketCapUsd).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "marketcapusd"]].Value);
            RuleFor(item => item.MaxSupply).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "maxsupply"]].Value);
            RuleFor(item => item.PriceUsd).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "priceusd"]].Value);
            RuleFor(item => item.Rank).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "rank"]].Value);
            RuleFor(item => item.Supply).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "supply"]].Value);
            RuleFor(item => item.VolumeUsd24Hr).NotNull().WithMessage(_stringLocalizer[language, "notNull", _stringLocalizer[language, "volumeusd24hr"]].Value);
            RuleFor(item => item.Name).Must((s) => AssetNameLogic.Instance.IsExist(s)).WithMessage(_stringLocalizer[language, "notExist"].Value);
            return this;
        }
    }
}
