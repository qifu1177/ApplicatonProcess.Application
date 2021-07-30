using ApplicatonProcess.Data;
using ApplicatonProcess.Data.Interfaces;
using ApplicatonProcess.Domain.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Logics
{
    public abstract class AbstractLogic<Request,Response>: ILogic<Request, Response> where Request:class where Response:class
    {
        protected IDataAccess _dataAccess;
        protected IJsonStringLocalizer _stringLocalizer;
        protected string _currentLanguage = "en";
        public AbstractLogic(IJsonStringLocalizer stringLocalizer, IDataAccess dataAccess)
        {
            _stringLocalizer = stringLocalizer;
            _dataAccess = dataAccess;
        }
        
        public abstract IEnumerable<Response> LoadResponses(object parentId=null);
        public abstract Response LoadResponse(object id);
        public abstract object Insert(Request request,object parentId=null);
        public abstract void Update(object id, Request request);
        public abstract void Delete(object id,object parentId=null);

        public void SetCurrentLanguage(string language)
        {
            _currentLanguage = language;
        }
    }
}
