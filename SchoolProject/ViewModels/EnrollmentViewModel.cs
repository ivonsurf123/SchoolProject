using System.ComponentModel.DataAnnotations;

namespace SchoolProject.ViewModels
{
    public class EnrollmentViewModel
    {
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Estudiante.")]
        public int StudentId { get; set; }

        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Curso.")]
        public int CourseId { get; set; }

        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }

        [Required(ErrorMessage = "El Periodo Académico es Obligatorio.")]
        [StringLength(20)]
        public string Period { get; set; } = string.Empty;

        public DateTime EnrollmentDate { get; set; }

        public bool IsActive { get; set; }
    }
}

