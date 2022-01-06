using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ActivityPlanner.Entities
{
    public class Activity
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int property_id { get; set; }

        [Required]
        public DateTime schedule { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        [Required]
        [StringLength(35)]
        public string status { get; set; }

        [ForeignKey(nameof(property_id))]
        public virtual Property Property { get; set; }

        public virtual Survey Survey { get; set; }

    }
}
