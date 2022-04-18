using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WeatherHelperLibrary;

namespace ITMD419_Project1Assignment.Pages
{
    public class ReadQueueModel : PageModel
    {
        private IConfiguration Configuration;

        public string Message { get; set; }

        public ReadQueueModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void OnGet()
        {
            var queueClient = new QueueClient(Configuration.GetConnectionString("AZStorage"), "itmd419rg1");

            // queueClient.SendMessage("this is a message");

            // Get the next message
            QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
            queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
            /* if (retrievedMessage.Length > 0)
              {
                  //var inputValue = Convert.ToDouble(retrievedMessage[0].MessageText);

                  WeatherInfo weatherInfo = JsonSerializer.Deserialize<WeatherInfo>{(retrievedMessage[0].MessageText)};

                  var ConversionResult1 = $"{InputValue} Fahrenheit is {TemperatureHelper.F2C(InputValue, true)} Celsius";

                  Message = ConversionResult1;


                  queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
              }
              else
              {
                  Message = "no queued messages";
              }

                  //if (string.IsNullOrEmpty(ModelState["InputValue"].AttemptedValue)
                   //   || string.IsNullOrWhiteSpace(ModelState["InputValue"].AttemptedValue))
                  //{
                  //    Message = $"blank is not valid";
                  //}
                  //else
                  //{
                  //    Message = $"{ModelState["InputValue"].AttemptedValue} is not valid";
                  //} */
            
            }
        }
    }

