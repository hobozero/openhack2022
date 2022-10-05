using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHack2022.Models
{
    public class Rating
    {
        internal static Rating Finalize(Rating rating)
        {
            return new Rating(rating.userId, rating.productId, rating.location, rating.rating, rating.userNotes)
            {
                RatingId = Guid.NewGuid(),
                TimeStamp = DateTime.UtcNow,
                Finalized = true
            };
        }

        public Rating(Guid userId, Guid productId, string location, int rating, string userNotes)
        {
            this.userId = userId;
            this.productId = productId;
            this.location = location;
            this.rating = rating;
            this.userNotes = userNotes;
        }

        internal bool Finalized { get; private set; }
        public Guid? RatingId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public Guid userId { get; private set; }
        public Guid productId { get; private set; }
        public string location { get; private set; }
        public int rating { get; private set; }
        public string userNotes { get; private set; }
    }

}
