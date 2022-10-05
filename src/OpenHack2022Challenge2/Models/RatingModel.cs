using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHack2022.Models
{
    public class RatingModel
    {
        internal static RatingModel Finalize(RatingModel rating)
        {
            return new RatingModel(rating.UserId, rating.ProductId, rating.Location, rating.Rating, rating.UserNotes)
            {
                RatingId = Guid.NewGuid(),
                TimeStamp = DateTime.UtcNow,
                Finalized = true
            };
        }

        public RatingModel(Guid userId, Guid productId, string location, int rating, string userNotes)
        {
            this.UserId = userId;
            this.ProductId = productId;
            this.Location = location;
            this.Rating = rating;
            this.UserNotes = userNotes;
        }

        internal bool Finalized { get; private set; }
        public Guid? RatingId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Location { get; private set; }
        public int Rating { get; private set; }
        public string UserNotes { get; private set; }
    }

}
