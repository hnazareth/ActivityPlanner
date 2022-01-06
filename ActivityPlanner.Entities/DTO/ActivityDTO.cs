using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.Entities.DTO
{
    public class ActivityDTO
    {
        public int id { get; set; }

        public int property_id { get; set; }

        public DateTime schedule { get; set; }

        public string title { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public string status { get; set; }
    }
}
