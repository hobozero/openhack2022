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
        }


        public Guid? id { get; set; }
        public DateTime timeStamp { get; set; }
        public Guid userId { get; set; }
        public Guid productId { get; set; } 
        public string locationName { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }
    }

}
