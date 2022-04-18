using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        
        private struct Input
        {
            public string ContentType { get; set; }
            public int Temperature { get; set; }
        }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var secretClient = new SecretClient(new Uri("https://project1assignment2022kv.vault.azure.net/"), new DefaultAzureCredential());

            var theSecret = secretClient.GetSecret("proj1secret");

            HttpClient httpClient = new HttpClient();
            //var getAFuncResult = await httpClient.GetAsync($"https://localhost:44369/api/TemperatureConverter?Temperature=32&ConvertType=C2F");
            //string resultAFuncString = await getAFuncResult.Content.ReadAsStringAsync();
           
            var input = new Input()
            {
                ContentType = "C2F",
                Temperature = 32

            };


           

            //var stringContent = new JsonContent<Input>()
            //var postResult = await httpClient.PostAsync($"https://localhost:5001/api/TemperatureConverter", stringContent);
        }
    }
}
