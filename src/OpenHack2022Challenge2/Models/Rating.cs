using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHack2022.Models
{
    public class Rating
    {
        public Guid userId { get; set; }
        public Guid productId { get; set; }
        public string location { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }
    }
}
