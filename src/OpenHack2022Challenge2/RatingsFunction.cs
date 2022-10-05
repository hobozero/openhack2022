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
using OpenHack2022.Application;
using OpenHack2022.Infrastructure;
using OpenHack2022.Models;

namespace OpenHack2022
{
    public class RatingsFunction
    {
        private readonly ILogger<RatingsFunction> _logger;
        private readonly RatingService _ratingService;


        public RatingsFunction(ILogger<RatingsFunction> log) 
        {
            _logger = log;
            _ratingService = new RatingService();
        }


        [FunctionName("CreateRatings")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "rating", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Rating** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> CreateRatings(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation($"Rating created test");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();


            try
            {
                var ratingResponse = await _ratingService.AddRating(requestBody);
                string responseMessage = JsonConvert.SerializeObject(ratingResponse.Data);
                return new OkObjectResult(responseMessage);
            }
            catch(OperationResponseException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}

