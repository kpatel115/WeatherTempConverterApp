using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WeatherHelperCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureConverterController : ControllerBase
    {
        private readonly ILogger<TemperatureConverterController> _logger;

        public class PostBody
        {
            public string ConvertType { get; set; }
            public int Temperature { get; set; }
        }
        /*[HttpPost] - lecture 3/28
        public double Post([FromBody]PostBody postBody)
        {
            if (ConvertType.Equals("C2F"))
                return WeatherHelperLibrary.TemperatureHelper.C2F(Temperature, true);
            else if (ConvertType.Equals("F2C"))
                return WeatherHelperLibrary.TemperatureHelper.F2C(Temperature, true);
            else
                throw new ArgumentException("Invalid convert type");
            
        }*/
        public TemperatureConverterController(ILogger<TemperatureConverterController> logger)
        {
            _logger = logger;
        }

        public class ConvertResult
        {

            [JsonPropertyName("Fahrenheit")]
            public double Fahrenheit { get; set; }
            [JsonPropertyName("Celsius")]
            public double Celsius { get; set; }
        }

        [HttpPost]
        public double Post([FromBody]PostBody postBody)
        {
            if (postBody.ConvertType.Equals("C2F"))
                return WeatherHelperLibrary.TemperatureHelper.C2F(postBody.Temperature, true);
            else if (postBody.ConvertType.Equals("F2C"))
                return WeatherHelperLibrary.TemperatureHelper.F2C(postBody.Temperature, true);
            else
                throw new ArgumentException("Invalid convert type");

        }

        [HttpGet]
        public ConvertResult Get(string ConvertType, int Temperature)
        {
            ConvertResult convertResult = new ConvertResult();

            if (ConvertType.Equals("C2F"))
            {
                convertResult.Celsius = Temperature;
                convertResult.Fahrenheit = WeatherHelperLibrary.TemperatureHelper.C2F(Temperature, true);
            }
            else if (ConvertType.Equals("F2C"))
            {
                convertResult.Fahrenheit = Temperature;
                convertResult.Celsius = WeatherHelperLibrary.TemperatureHelper.F2C(Temperature, true);
            }
                
            else
                throw new ArgumentException("Invalid convert type");

            return convertResult;

        }
    }
}
