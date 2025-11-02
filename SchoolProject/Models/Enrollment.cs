using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        // Foreign key to Student
        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }

        // Foreign key to Course
        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El Periodo Academico es Obligatorio")]
        [StringLength(20)]
        public string Period { get; set; } // e.g., "2025 - 1"

        public bool IsActive { get; set; } = true;

    }
}

