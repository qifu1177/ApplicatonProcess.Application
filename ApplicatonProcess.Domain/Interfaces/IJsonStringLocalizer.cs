using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Interfaces
{
    public interface IJsonStringLocalizer : IStringLocalizer
    {
        LocalizedString this[string language,string key, params object[] arguments] { get; }
        LocalizedString this[string language, string key] { get; }
    }
}
