using ApplicatonProcess.Domain.Attributes;
using ApplicatonProcess.Domain.Extensions;
using ApplicatonProcess.Domain.Helps;
using ApplicatonProcess.Domain.Interfaces;
using ApplicatonProcess.Domain.Logics;
using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Domain.Models.Examples;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicatonProcess.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Could not get the datas", typeof(ErrorResponse))]
    [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]

    public class AssetNamesController : ControllerBase
    {
        private readonly ILogger<AssetNamesController> _logger;
        private readonly IConfiguration _configuration;
        private IJsonStringLocalizer _stringLocalizer;
        private IAssetNameLogic _assetNameLogic;
        public AssetNamesController(IConfiguration config,ILogger<AssetNamesController> logger, IJsonStringLocalizer stringLocalizer, IAssetNameLogic assetNameLogic)
        {
            _stringLocalizer = stringLocalizer;
            _assetNameLogic = assetNameLogic;
            _configuration = config;
            _logger = logger;
        }
        // GET: api/<AssetNamesController>
        [HttpGet("{language}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get the List for names of asset from https://api.coincap.io/v2/assets", typeof(List<string>))]
        [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
        public async Task<IActionResult> Get(string language)
        {
            _assetNameLogic.SetCurrentLanguage(language);
            if (_assetNameLogic.AssetNames.Count>0)
            {
                return Ok(_assetNameLogic.AssetNames);
            }
            else
            {
                await _assetNameLogic.UpdateAssetNames(_configuration[Consts.ConstString.CONFIG_ASSETS_URL]);
                if (string.IsNullOrEmpty(_assetNameLogic.ErrorMessage))
                {
                    _logger.LogInformation("Get {0} names of asset from https://api.coincap.io/v2/assets.", _assetNameLogic.AssetNames.Count);
                    return Ok(_assetNameLogic.AssetNames);
                }
                else
                {
                    _logger.LogError(_assetNameLogic.Exception?.Message);
                    return BadRequest(new ErrorResponse { ErrorCode = (int)HttpStatusCode.NotFound, Message = _assetNameLogic.ErrorMessage });
                }
            }           
        }
    }
}
