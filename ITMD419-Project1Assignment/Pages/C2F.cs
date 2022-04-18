using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using WeatherHelperLibrary;
using Azure.Storage.Blobs;
using Azure.Data.Tables;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ITMD419_Project1Assignment.Pages
{
    public class C2F: PageModel // Celsius to Fahrenheit C2F 
    {

        private IConfiguration Configuration;
        private IWebHostEnvironment WebHostEnvironment;

        public C2F(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = hostingEnvironment;
        }

        public string Result { get; set; } = "";
        [BindProperty]
        [Required]

        public double? InputValue { get; set; }

        public async Task OnPost()
        {
            //Fahrenheit (F) = (Celsuis x 1.8) + 32

            if (ModelState.IsValid)
            {
                //var convertTemp = TemperatureHelper.C2F(InputValue.Value, true);
                //https://weatherhelpercorewebapi20220329124820.azurewebsites.net/api/TemperatureC2F?Temperature=22
                // var response = await httpClient.GetAsync($"http://localhost:46518/api/TemperatureC2F?Temperature={InputValue.Value.ToString()}");
                // https://weatherhelpercorewebapi20220329124820.azurewebsites.net/api/TemperatureC2F?Temperature={InputValue.Value.ToString()}
                
                HttpClient httpClient = new HttpClient();

                string targetUrl = "";
                if (WebHostEnvironment.IsDevelopment())
                {
                    //targetUrl = "http://localhost:46518/api/TemperatureC2F?Temperature";
                    targetUrl = "https://weatherhelpercorewebapi20220329124820.azurewebsites.net/api/TemperatureC2F?Temperature";
                }
                else
                {
                    targetUrl = "https://weatherhelpercorewebapi20220329124820.azurewebsites.net/api/TemperatureC2F?Temperature";
                }
                var response = await httpClient.GetAsync($"{targetUrl}={InputValue.Value.ToString()}");
                string resultAsString = await response.Content.ReadAsStringAsync();

                double.TryParse(resultAsString, out double convertTemp);

                Result = $"{InputValue.Value} Celcius is {convertTemp} Fahrenheit";
                var kelvin = InputValue.Value + 273.15;


                // New call to Get endpoint and deserialize POCO
                //var getPOCOResult = await httpClient.GetAsync($"http://localhost:46518/api/TemperatureConvert?ConvertType=C2F&Temperature={InputValue.Value.ToString()}");
                //string resultAsPOCOString = await getPOCOResult.Content.ReadAsStringAsync();
                //dynamic ConvertTemperature = JsonConvert.DeserializeObject(resultAsPOCOString);
                //double? f = ConvertTemperature.Fahrenheit;
                //f = ConvertTemperature.fahrenheit;
                //double? c = ConvertTemperature.Celsius;

                // call Azure Function
                //  http://localhost:7071/api/ConvertTemperatureC2F
                var getAFunctionResult = await httpClient.GetAsync($"https://temperatureconvertfunction20220402112125.azurewebsites.net/api/ConvertTemperatureC2F?code=AEd7QG91V2s9gn9Kcrtc8Sq3UG9w49IaU15a212Tg/k9FD/v580aSQ==&Temperature={InputValue.Value.ToString()}");
                string resultAFuncString = await getAFunctionResult.Content.ReadAsStringAsync();

                // create directory and write file //
                //if (!System.IO.Directory.Exists("Test")) System.IO.Directory.CreateDirectory("Test");

                //  System.IO.File.AppendAllText(@"Test\out.txt", $"{Result}\n");


                //var blobClient = new Azure.Storage.Blobs.BlobClient("DefaultEndpointsProtocol=https;AccountName=itmd419resourcegroup1;AccountKey=IfkH1Ta03euoiEDCrLtI6/8x8Q1Gt/LztDEedYiw1p68RlXsBnc93WoyJEQ4Q3WjjnzmFerqEbNq8Xbo/va1fg==;EndpointSuffix=core.windows.net", "itmd419weatherdetail",fileName);
                //blobClient.Upload(@"Test\out.txt", true);

                // write a blob to blob storage //
                var fileName = $"weather{Guid.NewGuid().ToString()}.text";
                var blobClient = new BlobClient(Configuration.GetConnectionString("AZStorage"), Configuration["AZStorageContainer"], fileName);

                var blobStorageClient = new BlobServiceClient(Configuration.GetConnectionString("AZStorage"));
                blobStorageClient.GetBlobContainerClient(Configuration["AZStorageContainer"]).CreateIfNotExists();

                //blobClient.Upload(@"Test\out.txt", true);
                var valid = blobClient.Exists();
                
                blobClient.Upload(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Result)));

                
                // write to table storage //

                var tableClient = new TableClient(Configuration.GetConnectionString("AZStorage"), Configuration["AZWeatherTable"]);

                tableClient.CreateIfNotExists();

                var now = System.DateTime.Now;
                var partitionKey = $"{now.Year}{now.Month}{now.Day}";
                var entity = new TableEntity(partitionKey, Guid.NewGuid().ToString())
                {
                    {"TemperatureInfo", Result },
                    {"Celsius", InputValue.Value },
                    { "Fahrenheit", convertTemp },
                    { "Kelvin", kelvin }
                };

                tableClient.AddEntity(entity);

                return ;
            }

            else
            {
                if (string.IsNullOrEmpty(ModelState["InputValue"].AttemptedValue)
                    || string.IsNullOrWhiteSpace(ModelState["InputValue"].AttemptedValue))
                {
                    Result = $"blank is not valid";
                }
                else
                {
                    Result = $"{ModelState["InputValue"].AttemptedValue} is not valid";
                }
                //Result = $"{ModelState["InputValue"].AttemptedValue} is not a valid temperature try again";
            }

            //if (InputValue != null)
            //{
            //   Result = $"Your input was {InputValue}";
            //}
            //else
            //{
            //    Result = "Your input was invalid";
            //}
        }
    }
}

 