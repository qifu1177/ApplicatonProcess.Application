using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Interfaces
{
    public interface IAssetNameLogic
    {
        void SetCurrentLanguage(string language);
        List<string> AssetNames { get; }
        Task UpdateAssetNames(string assetsUrl);
        bool IsExist(string value);
       string ErrorMessage { get; set; }
        Exception Exception { get; set; }
    }
}
