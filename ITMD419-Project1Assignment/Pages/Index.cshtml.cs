using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ITMD419_Project1Assignment.Pages
{
    public class IndexModel : PageModel
    {
        private struct Input
        {
            public string ConvertType { get; set; } 
            public int Temperature { get; set; } 
        }
        public async void OnGet()
        {
            HttpClient httpClient = new HttpClient();
            //var getAFuncResult = await httpClient.GetAsync($"https://localhost:44369/api/TemperatureConverter?Temperature=32&ConvertType=C2F");
            //string resultAFuncString = await getAFuncResult.Content.ReadAsStringAsync();
            var input = new Input()
            {
                ConvertType = "C2F",
                Temperature = 32

            };

               //var secretClient = new SecretClient(new Uri("https://project1assignment2022kv.vault.azure.net/"), new DefaultAzureCredential());

               //var theSecret = secretClient.GetSecret("proj1secret");

               var stringPayLoad = JsonConvert.SerializeObject(input);

               var stringContent = new StringContent(stringPayLoad, Encoding.UTF8, "application/json");

            //var stringContent = new JsonContent<Input>()
            var postResult = await httpClient.PostAsync($"https://localhost:46518/api/TemperatureConverter", stringContent);
            string resultAFuncString = await postResult.Content.ReadAsStringAsync();

            var ConvertTemperature = JsonConvert.DeserializeObject(resultAFuncString);


        }
    }
}
