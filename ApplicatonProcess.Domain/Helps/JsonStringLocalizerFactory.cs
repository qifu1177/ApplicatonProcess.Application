using ApplicatonProcess.Domain.Interfaces;
using Microsoft.Extensions.Localization;
using System;


namespace ApplicatonProcess.Domain.Helps
{
    
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer();
        }

        
    }
}
