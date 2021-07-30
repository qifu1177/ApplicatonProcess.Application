using ApplicatonProcess.Data;
using ApplicatonProcess.Domain.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Logics
{
    public class AssetNameLogic : Singleton<AssetNameLogic>, IAssetNameLogic
    {
        private IJsonStringLocalizer _stringLocalizer;
        private List<string> _assetNames = new List<string>();
        private string _currentLanguage="en";
        public AssetNameLogic SetStringLocalizer(IJsonStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            return this;
        }
        public List<string> AssetNames => _assetNames;
        public async Task UpdateAssetNames(string assetsUrl)
        {
            try
            {
                ErrorMessage = string.Empty;
                Exception = null;
                _assetNames.Clear();
                _assetNames.AddRange(await HTTPDataAccess.Instance.LoadAssetNames(assetsUrl));
            }
            catch(Exception ex)
            {
                Exception = ex;
                ErrorMessage = _stringLocalizer[_currentLanguage,"loadAssetsFromUrlError"];
            }
            
        }
        public bool IsExist(string value)
        {
            return AssetNames.Contains(value);
        }

        public void SetCurrentLanguage(string language)
        {
            _currentLanguage = language;
        }

        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
    }
}
