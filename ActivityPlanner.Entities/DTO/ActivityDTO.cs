using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.Entities.DTO
{
    public class ActivityDTO
    {
        public int ID { get; set; }

        public DateTime schedule { get; set; }

        public string title { get; set; }

        public DateTime created_at { get; set; }

        public string status { get; set; }

        public string condition { get; set; }

        public object property { get; set; }
        public string survey { get; set; }
    }
}
