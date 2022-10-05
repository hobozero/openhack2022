using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHack2022.Models
{
    public class RatingModel
    {
        internal void SetId()
        {
            id = Guid.NewGuid();
            timeStamp = DateTime.UtcNow;
            Finalized = true;
        }

        public RatingModel(Guid userId, Guid productId, string locationName, int rating, string userNotes)
        {
            this.userId = userId;
            this.productId = productId;
            this.locationName = locationName;
            this.rating = rating;
            this.userNotes = userNotes;
        }

        internal bool Finalized { get; private set; }
        public Guid? id { get; private set; }
        public DateTime timeStamp { get; private set; }
        public Guid userId { get; private set; }
        public Guid productId { get; private set; }
        public string locationName { get; private set; }
        public int rating { get; private set; }
        public string userNotes { get; private set; }
    }

}
