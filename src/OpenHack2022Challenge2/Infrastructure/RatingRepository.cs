using OpenHack2022.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenHack2022.Infrastructure
{
    public class RatingRepository : IRatingRepository
    {
        private readonly string _connectionString;

        public RatingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<Rating> CreateRating(Rating rating)
        {
            //TODO: save rating to data store 

            return Rating.Finalize(rating);
        }

        public async Task<Rating> GetRating(Guid ratingId)
        {
            //TODO: get rating from data store
            return RatingRepository.DeletemeTestData();
        }
        public async Task<IEnumerable<Rating>> GetRatings()
        {
            //TODO: get ratings from data store
            return new List<Rating>() { RatingRepository.DeletemeTestData() };
        }

        public static Rating DeletemeTestData()
        {
            var unsavedRating = new Rating(
                Guid.Parse("6dd3bb49-a5be-41ca-9dac-3b995450f2db"),
                Guid.Parse("65ab124a-9b2c-4294-a52d-18839364ef15"),
                "US West",
                1,
                "test notes");

            return Rating.Finalize(unsavedRating);

        }
    }
}
