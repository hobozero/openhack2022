using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenHack2022.Infrastructure;
using OpenHack2022.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenHack2022.Application
{
    public class RatingService
    {
        private readonly IRatingRepository _repo;
        private readonly ILogger<RatingService> _logger;
        private readonly HttpClient _httpClient;

        public RatingService(ILogger<RatingService> logger, IRatingRepository repo, HttpClient httpClient)
        {
            _repo = repo;
            _logger = logger;
            _httpClient = httpClient;
        }
            
        //public RatingService()
        //{
        //    //TODO: get creds from secret store
        //    //TODO: add connection string to config file
        //    string connectionString = string.Empty;
        //    _repo = new RatingRepository(connectionString);
        //}

        public async Task<Result<Rating>> AddRating(Rating rating)
        {
            //TODO: replace with fluent Validation?
            if (rating.rating > 5)
            {
                return rating.ToErrorResult("rating too high", new Dictionary<string, string>() { { "rating", rating.rating.ToString() } });
            }
            if (rating.rating < 1)
            {
                return rating.ToErrorResult("rating too low", new Dictionary<string, string>() { { "rating", rating.rating.ToString() } });
            }

            //TODO: Get base URL from config
            var userResult = await _httpClient.GetAsync($"https://serverlessohapi.azurewebsites.net/api/GetUser?userId={rating.userId}");
            if (userResult.StatusCode != HttpStatusCode.OK)
            {
                return "user not found".ToErrorResult<Rating>(new Dictionary<string, string>() { { "user", rating.userId.ToString() } });
            }

            //TODO: Get base URL from config
            var productResult = await _httpClient.GetAsync($"https://serverlessohapi.azurewebsites.net/api/GetProduct?productId={rating.productId}");
            if (productResult.StatusCode != HttpStatusCode.OK)
            {
                return "product not found".ToErrorResult<Rating>(new Dictionary<string, string>() { { "product", rating.productId.ToString() } });
            }

            rating = await _repo.CreateRating(rating);

            return rating.ToSuccessResult();
        }

        public async Task<Result<Rating>> AddRating(string submission)
        {
            Rating ratingPosted = null;
            try
            {
                 ratingPosted = JsonConvert.DeserializeObject<Rating>(submission);
            }
            catch (JsonReaderException jex)
            {
                return "There was an error parsing rating".ToErrorResult<Rating>();
            }
            catch (JsonSerializationException jex)
            {
                return "There was an error parsing rating".ToErrorResult<Rating>();
            }

            return await AddRating(ratingPosted);
           
        }
    }
}
