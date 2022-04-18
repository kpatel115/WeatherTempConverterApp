using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherHelperCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureC2FController : ControllerBase
    {
        private readonly ILogger<TemperatureC2FController> _logger;

        public TemperatureC2FController(ILogger<TemperatureC2FController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public double Get(int Temperature, bool shouldRound = true)
        {
                
                return WeatherHelperLibrary.TemperatureHelper.C2F(Temperature, shouldRound);
            
        }
    }
}