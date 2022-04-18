using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherHelperLibrary;
using Azure.Storage.Blobs;
using Azure.Data.Tables;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace ITMD419_Project1Assignment.Pages
{
    public class F2C : PageModel // Fahrenheit to Celsius  
    {
        private IConfiguration Configuration;

        public F2C(IConfiguration configuration, ILogger<F2C> logger, ILogger<F2C> Logger)
        {
            Configuration = configuration;
            Logger = logger;
        }
        public string ConversionResult { get; set; }

        [BindProperty]
        [Required]

        public double? InputValue { get; set; }
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                // C = (f-32) * 5/9
                ConversionResult = $"{InputValue} Fahrenheit is {TemperatureHelper.F2C(InputValue.Value, true)} Celsius";

                var queueClient = new QueueClient(Configuration.GetConnectionString("AZStorage"), "itmd419rg1" );

                queueClient.SendMessage("this is a message");

                //var weatherInfo = new WeatherInfo();
                //{
                //    Fahrenheit = InputValue.Value,
                //        Description = ConversionResult,
                //};
                //var serializedObj = JsonSerializer.Serialize(weatherInfo);
                //Logger.LogInformation($"Serialized object is {serializedObj}");
                //Logger.LogError($"Serialized object is {serializedObj}");
                //System.Console.WriteLine($"Serialized object is {serializedObj}");

                //queueClient.SendMessage(serializedObj);
            }
            else
            {
                if (string.IsNullOrEmpty(ModelState["InputValue"].AttemptedValue) 
                    || string.IsNullOrWhiteSpace(ModelState["InputValue"].AttemptedValue))
                     {
                    ConversionResult = $"blank is not valid";
                }
                else {
                    ConversionResult = $"{ModelState["InputValue"].AttemptedValue} is not valid";
                }
            }
        }
    }
}
