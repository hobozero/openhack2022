using System.Collections.Generic;
using System.IO;
using System.Linq;
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


        public RatingsFunction(ILogger<RatingsFunction> log, RatingService ratingService) 
        {
            _logger = log;
            _ratingService = ratingService;
        }


        [FunctionName("CreateRatings")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "rating", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Rating** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult CreateRatings(
            [CosmosDB(
                databaseName: "Ratings",
                collectionName: "CustomerRating2",
                ConnectionStringSetting = "CosmosDBConnectionString")]out dynamic document,
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation($"Rating created test");

            string requestBody = new StreamReader(req.Body).ReadToEnd();

            try
            {
                var ratingResponse = _ratingService.AddRating(requestBody);
                if (ratingResponse.Success)
                {
                    ratingResponse.Data.SetId();
                    document = ratingResponse.Data;
                    string responseMessage = JsonConvert.SerializeObject(ratingResponse.Data);
                    return new OkObjectResult(responseMessage);
                }
                else
                {
                    document=null;
                    return new BadRequestObjectResult(ratingResponse.ErrorMessage);
                }
                
            }
            catch(OperationResponseException ex)
            {
                document = null;
                return new BadRequestObjectResult(ex.Message);
            }
        }



        [FunctionName("Rating")]
        [OpenApiOperation(operationId: "GetRating", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "rating", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Rating** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult GetRating(
            [CosmosDB(
                databaseName: "Ratings",
                collectionName: "CustomerRating2",
                ConnectionStringSetting = "CosmosDBConnectionString",
                SqlQuery = "select * from r where r.id = {id}" //{Query.userId}
                //Id = "{Query.id}",
                //PartitionKey = "{Query.productId}"
            )]IEnumerable<RatingModel> ratingDocs,
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rating/id/{id}")] HttpRequest req)
        {
            _logger.LogInformation($"Rating queried");

            if (ratingDocs?.Count() == 0)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(ratingDocs.FirstOrDefault());
            }
        }

        [FunctionName("Ratings")]
        [OpenApiOperation(operationId: "GetRatings", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "rating", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Rating** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult GetRatings(
            [CosmosDB(
                databaseName: "Ratings",
                collectionName: "CustomerRating2",
                ConnectionStringSetting = "CosmosDBConnectionString",
                SqlQuery = "select * from r where r.userId = {userId}" //{Query.userId}
                //Id = "{Query.id}",
                //PartitionKey = "{Query.productId}"
            )]IEnumerable<RatingModel> ratingDocs,

            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rating/user/{userId}")] HttpRequest req)
        {
            _logger.LogInformation($"Rating queried");

            if (ratingDocs.Count() == 0)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(ratingDocs);
            }
        }
    }
}

