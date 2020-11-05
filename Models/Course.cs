namespace FinalAssignment2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Score { get; set; }

        [Range(0,10)]
        public int Score_num { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int TutorId { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}
