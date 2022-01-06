using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ActivityPlanner.Entities
{
    public class Property
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]        
        public string address { get; set; }

        [Required]
        public string description { get; set; }
            
        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }
        
        public DateTime? disabled_at { get; set; }

        [Required]
        [StringLength(35)]
        public string status { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
    }
}
