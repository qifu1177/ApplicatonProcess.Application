
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Models;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Helps
{
    
    public class JsonStringLocalizer : IJsonStringLocalizer
    {
        List<JsonLocalization> localization = new List<JsonLocalization>();
        private string _currentLanguage="en";
        public JsonStringLocalizer()
        {
            //read all json file
            //JsonSerializer serializer = new JsonSerializer();
            localization = JsonConvert.DeserializeObject<List<JsonLocalization>>(EmbeddedResource.GetApiRequestFile(Assembly.GetExecutingAssembly(),@"translation.json"));

        }

        public JsonStringLocalizer(string jsonStr)
        {
            localization = JsonConvert.DeserializeObject<List<JsonLocalization>>(jsonStr);

        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public LocalizedString this[string language, string key]
        {
            get
            {
                var value = GetString(key,language);
                return new LocalizedString(key, value ?? key, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string language, string key, params object[] arguments]
        {
            get
            {
                var format = GetString(key,language);
                var value = string.Format(format ?? key, arguments);
                return new LocalizedString(key, value, resourceNotFound: format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return localization.Where(l => l.LocalizedValue.Keys.Any(lv => lv == _currentLanguage)).Select(l => new LocalizedString(l.Key, l.LocalizedValue[_currentLanguage], true));
        }

        public void SetCurrentLanguage(string language)
        {
            _currentLanguage = language;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer();
        }

        private string GetString(string name, string language="en")
        {            
            var query = localization.Where(l => l.LocalizedValue.Keys.Any(lv => lv == language));
            var value = query.FirstOrDefault(l => l.Key == name);
            if(value==null || !value.LocalizedValue.ContainsKey(language)){
                return name;
            }
            return value.LocalizedValue[language];
        }

    }
   
}
