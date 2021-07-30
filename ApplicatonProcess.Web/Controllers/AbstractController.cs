using FluentValidation;
using ApplicatonProcess.Domain.Attributes;
using ApplicatonProcess.Domain.Extensions;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Logics;
using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Domain.Models.Examples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ApplicatonProcess.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Could not get the datas", typeof(ErrorResponse))]
    [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
    public abstract class AbstractController<C,Request,Response> : ControllerBase where C : ControllerBase where Response:class where Request:class
    {
        protected readonly ILogger<C> _logger;
        protected readonly IConfiguration _configuration;
        //protected string _dbName;
        protected ILogic<Request,Response> _logic;
        protected IJsonStringLocalizer _stringLocalizer;
        protected IValidatorWithTranslator<Request> _validator;
        public AbstractController(IConfiguration configuration, ILogger<C> logger, IJsonStringLocalizer stringLocalizer, ILogic<Request, Response> logic, IValidatorWithTranslator<Request> validator)
        {
            _logic = logic;
            _stringLocalizer = stringLocalizer;
            _configuration = configuration;           
            _logger = logger;
            _validator = validator;

        }

        protected (bool, IActionResult) Validat(Request request,string language)
        {            
            var result = _validator.Init(language).Validate(request);
            if (result.Errors.Count > 0)
            {
                ErrorMessagesResponse errorMessagesResponse = new ErrorMessagesResponse();
                errorMessagesResponse.ErrorCode = (int)HttpStatusCode.BadRequest;
                foreach (var failure in result.Errors)
                {
                    errorMessagesResponse.Messages.Add(failure.ErrorMessage);
                }
                return (false, BadRequest(errorMessagesResponse));
            }
            return (true, null);
        }
    }
}
