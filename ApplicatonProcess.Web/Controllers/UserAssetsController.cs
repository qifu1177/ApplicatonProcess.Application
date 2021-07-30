using FluentValidation;
using ApplicatonProcess.Domain.Attributes;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Logics;
using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Domain.Models.Examples;
using ApplicatonProcess.Domain.Models.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace ApplicatonProcess.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Could not get the datas", typeof(ErrorResponse))]
    [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
    public class UserAssetsController : AbstractController<UserAssetsController, AssetRequest, AssetResponse>
    {
        public UserAssetsController(IConfiguration configuration, ILogger<UserAssetsController> logger, IJsonStringLocalizer stringLocalizer, ILogic<AssetRequest, AssetResponse> logic, IValidatorWithTranslator<AssetRequest> validator) : base(configuration, logger, stringLocalizer, logic, validator)
        {
            
        }
        [HttpGet("{email}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully get the assets", typeof(List<AssetResponse>))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(AssetResponseExample))]
        public IActionResult Get(string email,string language)
        {
            _logic.SetCurrentLanguage(language);
            try
            {
                return Ok(_logic.LoadResponses(email));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpGet("id/{id}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully get the asset", typeof(AssetResponse))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(AssetResponseExample))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        public IActionResult Get(int id,string language)
        {
            _logic.SetCurrentLanguage(language);
            try
            {
                return Ok(_logic.LoadResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("{email}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Successfully save the asset")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The asset could not be validated.", typeof(ErrorResponse))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        [SwaggerRequestExample(typeof(AssetRequest), typeof(AssetRequestExample))]
        public IActionResult Post(string email,string language, AssetRequest request)
        {
            _logic.SetCurrentLanguage(language);
            try
            {
                (bool b, IActionResult actionResult) = Validat(request,language);
                if (!b)
                    return actionResult;
                object assetId=_logic.Insert(request,email);
                return Created("", new ResultResponse { Result = assetId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPut("{id}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully save the asset")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The asset could not be validated.", typeof(ErrorResponse))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        [SwaggerRequestExample(typeof(AssetRequest), typeof(AssetRequestExample))]
        public IActionResult Put(int id, string language, AssetRequest request)
        {
            _logic.SetCurrentLanguage(language);
            try
            {
                (bool b, IActionResult actionResult) = Validat(request,language);
                if (!b)
                    return actionResult;
                _logic.Update(id, request);
                return Ok(new ResultResponse { Result = "OK" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });
            }

        }

        [HttpDelete("{email}/{id}/{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully delete the asset")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "The id could not found.", typeof(ErrorResponse))]
        [SwaggerResponseExample(HttpStatusCode.BadRequest, typeof(NotFoundResponseExample))]
        public IActionResult Delete(string email,int id,string language)
        {
            _logic.SetCurrentLanguage(language);
            try
            {
                _logic.Delete(id,email);
                return Ok(new ResultResponse { Result = "OK" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message);
                return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.BadRequest, Message = ex.Message });

            }
        }
       
    }
}
