using System;
using System.Collections.Generic;
using System.Text;

namespace ActivityPlanner.Entities.DTO
{
    public class SurveyDTO
    {
        public int id { get; set; }
        public int activity_id { get; set; }
        public string answers { get; set; }
    }
}
