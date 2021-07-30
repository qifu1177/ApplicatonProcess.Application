using ApplicatonProcess.Domain.Helps;
using ApplicatonProcess.Domain.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Extensions
{
    public static class JsonStringLocalizerFactoryExtension
    {
        public static IJsonStringLocalizer Create(this IStringLocalizerFactory factory, Assembly assembly,string namespaceAndFileName)
        {
            string json = EmbeddedResource.GetApiRequestFile(assembly,namespaceAndFileName);
            return new JsonStringLocalizer(json);
        }
    }
}
