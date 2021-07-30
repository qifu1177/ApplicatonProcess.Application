using FluentValidation;
using ApplicatonProcess.Domain.Attributes;
using ApplicatonProcess.Domain.Extensions;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Logics;
using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Domain.Models.Examples;
using ApplicatonProcess.Domain.Models.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicatonProcess.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Could not get the datas", typeof(ErrorResponse))]
    [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
    public class UsersController : AbstractController<UsersController, UserRequest, UserResponse>
    {

        public UsersController(IConfiguration configuration, ILogger<UsersController> logger, IJsonStringLocalizer stringLocalizer, ILogic<UserRequest, UserResponse> logic, IValidatorWithTranslator<UserRequest> validator) : base(configuration, logger, stringLocalizer, logic, validator)
        {         
        
        }

        [HttpGet("{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully get the users", typeof(List<UserResponse>))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(UserResponseExample))]
        public IActionResult Get(string language)
        {
            _logic.SetCurrentLanguage(language);
            try
            {                
                return Ok(_logic.LoadResponses());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }


        [HttpGet("id/{email}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully get the user", typeof(UserResponse))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(UserResponseExample))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        public IActionResult Get(string email,string language)
        {
            _logic.SetCurrentLanguage(language);
            try
            {                
                return Ok(_logic.LoadResponse(email));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }


        [HttpPost("{language}")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Successfully save the user")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The user could not be validated.", typeof(ErrorResponse))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        [SwaggerRequestExample(typeof(UserRequest), typeof(UserRequestExample))]
        public IActionResult Post(string language, UserRequest request)
        {
            _logic.SetCurrentLanguage(language);
            try
            {
                (bool b, IActionResult actionResult) = Validat(request,language);
                if (!b)
                    return actionResult;
                _logic.Insert(request);
                return Created("", new ResultResponse { Result = request.Email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPut("{email}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully save the user")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The user could not be validated.", typeof(ErrorResponse))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        [SwaggerRequestExample(typeof(UserRequest), typeof(UserRequestExample))]
        public IActionResult Put(string email, string language, UserRequest request)
        {
           
            _logic.SetCurrentLanguage(language);
            try
            {
                (bool b, IActionResult actionResult) = Validat(request,language);
                if (!b)
                    return actionResult;
                _logic.Update(email, request);
                return Ok(new ResultResponse { Result = "OK" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpDelete("{email}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully save the user")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The email could not be validated.", typeof(ErrorResponse))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        public IActionResult Delete(string email,string language)
        {
            _logic.SetCurrentLanguage(language);
            
            try
            {
                _logic.Delete(email);
                return Ok(new ResultResponse { Result = "OK" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });

            }
        }
        //private (bool, IActionResult) Validat(UserRequest request)
        //{
        //    var validator = new UserValidator(_stringLocalizer);
        //    var result = validator.Validate(request);
        //    if (result.Errors.Count > 0)
        //    {
        //        ErrorMessagesResponse errorMessagesResponse = new ErrorMessagesResponse();
        //        errorMessagesResponse.ErrorCode = (int)HttpStatusCode.BadRequest;
        //        foreach (var failure in result.Errors)
        //        {
        //            errorMessagesResponse.Messages.Add(failure.ErrorMessage);
        //        }
        //        return (false, BadRequest(errorMessagesResponse));
        //    }
        //    return (true, null);
        //}

    }
}
