using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TemperatureHelperLibrary;

namespace TemperatureConvertFunction
{
    public static class Function1
    {
        [FunctionName("EchoMessage")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // if its a get query string parameter
            string name = req.Query["name"];

            // if its a post check for the body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            // either use the get parameter or the post parameter
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("ConvertTemperatureC2F")]
        [OpenApiOperation(operationId: "ConvertTemperatureC2F", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "Temperature", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "The **Temperature** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult ConvertTemperature(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "";
            double convertedTemperature = 0;

            // if its a get query string parameter
            string temp = req.Query["Temperature"];

           if (string.IsNullOrEmpty(temp))
            {
                responseMessage = "This HTTP triggered function executed successfully. It needs the Temperature Argument";
                return new NotFoundObjectResult(responseMessage);
            }
           else
            {
                convertedTemperature = TemperatureHelper.C2F(System.Convert.ToDouble(temp));
            }
            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(convertedTemperature.ToString());
        }


        [FunctionName("ConvertTemperatureF2C")]
        [OpenApiOperation(operationId: "ConvertTemperatureF2C", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "Temperature", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "The **Temperature** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult ConvertTemperatureF2C(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "";
            double convertedTemperature = 0;

            // if its a get query string parameter
            string temp = req.Query["Temperature"];

            if (string.IsNullOrEmpty(temp))
            {
                responseMessage = "This HTTP triggered function executed successfully. It needs the Temperature Argument";
                return new NotFoundObjectResult(responseMessage);
            }
            else
            {
                convertedTemperature = TemperatureHelper.F2C(System.Convert.ToDouble(temp));
            }
            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(convertedTemperature.ToString());
        }

    }
}

