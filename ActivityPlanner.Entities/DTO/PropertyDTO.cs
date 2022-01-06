using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ActivityPlanner.Entities.DTO
{
    public class PropertyDTO
    {
        public int id { get; set; }

        public string title { get; set; }

        public string address { get; set; }

        public string description { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime? disabled_at { get; set; }

        public string status { get; set; }
    }
}
