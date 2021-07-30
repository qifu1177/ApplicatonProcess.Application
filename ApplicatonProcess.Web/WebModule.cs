using Autofac;
using ApplicatonProcess.Domain;
using ApplicatonProcess.Domain.Helps;
using ApplicatonProcess.Domain.Extensions;
using Microsoft.Extensions.Localization;
using System.Reflection;
using ApplicatonProcess.Domain.Logics;
using ApplicatonProcess.Domain.Interfaces;

namespace ApplicatonProcess.Web
{
    public class WebModule : Autofac.Module
    {
        private string _dbName;
        private string _assetUrl;
        public WebModule(string dbName,string assetUrl)
        {
            _dbName = dbName;
            _assetUrl = assetUrl;
        }
        protected override void Load(ContainerBuilder builder)
        {           
            IStringLocalizerFactory stringLocalizerFactory = new JsonStringLocalizerFactory();
            string nameSpace=this.GetType().Namespace;
            var jsonStringLocalizer = stringLocalizerFactory.Create(Assembly.GetExecutingAssembly(), $"{nameSpace}.Resources.translation.json");
            builder.RegisterInstance(jsonStringLocalizer).As<IJsonStringLocalizer>().ExternallyOwned();
            AssetNameLogic.Instance.SetStringLocalizer(jsonStringLocalizer).UpdateAssetNames(_assetUrl);
            builder.RegisterInstance(AssetNameLogic.Instance).As<IAssetNameLogic>().ExternallyOwned();
            builder.RegisterModule(new DomainModule(_dbName));
        }
    }
}
