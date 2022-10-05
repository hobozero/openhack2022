using OpenHack2022.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenHack2022.Infrastructure
{
    public interface IRatingRepository
    {
        Task<Rating> CreateRating(Rating rating);
        Task<Rating> GetRating(Guid ratingId);
        Task<IEnumerable<Rating>> GetRatings();
    }
}