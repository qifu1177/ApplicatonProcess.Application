using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Interfaces
{
    public interface ILogic<Request, Response> where Request : class where Response : class
    {
        //void Init(string dbName, IStringLocalizer stringLocalizer);
        void SetCurrentLanguage(string language);
        IEnumerable<Response> LoadResponses(object parentId = null);
        Response LoadResponse(object id);
        object Insert(Request request, object parentId = null);
        void Update(object id, Request request);
        void Delete(object id, object parentId = null);
    }
}
