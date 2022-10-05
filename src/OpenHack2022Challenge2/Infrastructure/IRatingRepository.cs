using OpenHack2022.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenHack2022.Infrastructure
{
    public interface IRatingRepository
    {
        Task<RatingModel> CreateRating(RatingModel rating);
        Task<RatingModel> GetRating(Guid ratingId);
        Task<IEnumerable<RatingModel>> GetRatings();
    }
}