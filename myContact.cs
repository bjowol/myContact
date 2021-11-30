using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace my.Contact
{
    public static class myContact
    {
        [FunctionName("myContact")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            Contact con = new Contact();
            string json = con.GetMeJson();

            return new OkObjectResult(json);
        }
    }

    public class Contact {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string PhoneNumber {get; set;}
        public string EmailAddress {get; set;}

        public void GetMe (){
            this.FirstName = "Bjørn Milian";
            this.LastName = "Wolstad";
            this.EmailAddress = "bmw@aztek.no";
            this.PhoneNumber = "+47 974 70 441";
        }

        public string GetMeJson () {
            this.FirstName = "Bjørn Milian";
            this.LastName = "Wolstad";
            this.EmailAddress = "bmw@aztek.no";
            this.PhoneNumber = "+47 974 70 441";
            
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
