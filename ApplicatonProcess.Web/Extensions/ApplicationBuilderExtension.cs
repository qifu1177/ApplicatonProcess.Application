using ApplicatonProcess.Domain.Logics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicatonProcess.Web.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> InitDatas(this IApplicationBuilder app, string assetsUrl)
        {
            await AssetNameLogic.Instance.UpdateAssetNames(assetsUrl);
            return app;
        }
    }    
}
