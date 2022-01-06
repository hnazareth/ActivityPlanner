using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ActivityPlanner.Entities
{
    public class Survey
    {
        [Key]
        public int id { get; set; }
        
        [Required]        
        public int activity_id  { get; set; }

        [Required]
        [Column(TypeName="json")]
        public string answers { get; set; }

        [ForeignKey(nameof(activity_id))]
        public virtual Activity Activity { get; set; }

    }
}
