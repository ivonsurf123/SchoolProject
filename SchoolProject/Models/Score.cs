using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }

        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        [Required(ErrorMessage = "El tipo de Evaluacion es Obligatorio.")]
        [StringLength(50)]
        public string EvaluationType { get; set; } // e.g., "Exam", "Quiz", "Assignment"

        [Required(ErrorMessage = "La Calificacion es Obligatoria.")]
        [Range(0, 100, ErrorMessage = "La Calificacion debe estar entre 0 y 100.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "La Fecha es Obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El Periodo Academico es Obligatorio")]
        [StringLength(20)]
        public string Period { get; set; } // e.g., "2025 - 1"

        [StringLength(200)]
        public string? Observations { get; set; }

        [NotMapped]
        public bool IsApproved => Value >= 70; // Assuming 70 is the passing score
    }
}
