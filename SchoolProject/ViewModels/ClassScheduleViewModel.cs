using System.ComponentModel.DataAnnotations;

namespace SchoolProject.ViewModels
{
    public class ClassScheduleViewModel
    {
        public int ClassScheduleId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Curso.")]
        public int CourseId { get; set; }

        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? ProfessorName { get; set; }

        [Required(ErrorMessage = "El Día es Obligatorio.")]
        [StringLength(12)]
        public string Day { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Hora de Inicio es Obligatoria.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "La Hora de Fin es Obligatoria.")]
        public DateTime EndTime { get; set; }

        // Propiedades auxiliares para mostrar solo la hora
        public string StartTimeDisplay => StartTime.ToString("HH:mm");
        public string EndTimeDisplay => EndTime.ToString("HH:mm");

        [Required(ErrorMessage = "El Aula es Obligatoria.")]
        [StringLength(20)]
        public string Classroom { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Period { get; set; }

        public bool IsActive { get; set; }
    }
}

