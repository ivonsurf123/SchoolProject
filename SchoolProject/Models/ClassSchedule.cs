using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models
{
    public class ClassSchedule
    {
        [Key]
        public int ClassScheduleId { get; set; }

        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        [Required(ErrorMessage = "El Día es Obligatorio.")]
        [StringLength(12)]
        public string Day { get; set; }

        [Required(ErrorMessage = "La Hora de Inicio es Obligatoria.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "La Hora de Fin es Obligatoria.")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "El Aula es Obligatoria.")]
        [StringLength(20)]
        public string Classroom { get; set; }

        [StringLength(20)]
        public string Period { get; set; } // e.g., "2025 - 1"
        public bool IsActive { get; set; } = true;
    }
}

