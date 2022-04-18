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
    public class TemperatureF2CController : ControllerBase
    {
        private readonly ILogger<TemperatureF2CController> _logger;

        public TemperatureF2CController(ILogger<TemperatureF2CController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public double Get(int Temperature, bool shouldRound=true)
        {
                return WeatherHelperLibrary.TemperatureHelper.F2C(Temperature, shouldRound);
            
        }
    }
}
