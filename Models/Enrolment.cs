namespace FinalAssignment2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Enrolment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Start { get; set; }

        public string UserId { get; set; }

        public virtual Course Course { get; set; }
    }
}
