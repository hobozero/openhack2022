using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenHack2022.Infrastructure;
using OpenHack2022.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenHack2022.Application
{
    public class RatingService
    {
        private readonly IRatingRepository _repo;
        private readonly ILogger<RatingService> _logger;

        public RatingService(ILogger<RatingService> logger, IRatingRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }
            
        public RatingService()
        {
            //TODO: get creds from secret store
            //TODO: add connection string to config file
            string connectionString = string.Empty;
            _repo = new RatingRepository(connectionString);
        }

        public async Task<Result<Rating>> AddRating(Rating ratingPosted)
        {
            //TODO: replace with fluent Validation?
            if (ratingPosted.rating > 5)
            {
                return ratingPosted.ToErrorResult("rating too high", new Dictionary<string, string>() { { "rating", ratingPosted.rating.ToString() } });
            }
            if (ratingPosted.rating < 1)
            {
                return ratingPosted.ToErrorResult("rating too low", new Dictionary<string, string>() { { "rating", ratingPosted.rating.ToString() } });
            }

            ratingPosted = await _repo.CreateRating(ratingPosted);

            return ratingPosted.ToSuccessResult();
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
