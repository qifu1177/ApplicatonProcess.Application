using Autofac;
using FluentValidation;
using ApplicatonProcess.Data;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Logics;
using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Domain.Models.Validators;
using Microsoft.Extensions.Localization;

namespace ApplicatonProcess.Domain
{
    public class DomainModule : Module
    {
        private string _dbName;

        public DomainModule(string dbName)
        {
            _dbName = dbName;
            //_stringLocalizer = stringLocalizer;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataModule(_dbName));
            builder.RegisterType<AssetValidator>().As<IValidatorWithTranslator<AssetRequest>>();
            builder.RegisterType<UserValidator>().As<IValidatorWithTranslator<UserRequest>>();
            builder.RegisterType<UserLogic>().As<ILogic<UserRequest, UserResponse>>();
            builder.RegisterType<UserAssetLogic>().As<ILogic<AssetRequest, AssetResponse>>();
        }
    }
}
