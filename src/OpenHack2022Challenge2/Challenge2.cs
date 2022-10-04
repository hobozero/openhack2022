using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OpenHack2022Challenge2
{
    public static class Challenge2
    {
        [FunctionName("echo")]
        public static async Task<IActionResult> RunName(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

           string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"The product name for your product id {name} is Starfruit Explosion and the description is This starfruit ice cream is out of this world!";

            return new OkObjectResult(responseMessage);
        }

        //[FunctionName("productId")]
        //public static async Task<IActionResult> RunProductId(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);

        //    var prodId = data?.productId;

        //    string responseMessage = string.IsNullOrEmpty(prodId)
        //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //        : $"The product name for your product id {prodId} is Starfruit Explosion";

        //    return new OkObjectResult(responseMessage);
        //}

    }
}
