using System.ComponentModel.DataAnnotations;

namespace SchoolProject.ViewModels
{
    public class ScoreViewModel
    {
        public int ScoreId { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un Estudiante.")]
        public int StudentId { get; set; }
        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un Curso.")]
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        [Required(ErrorMessage = "El Tipo de Evaluación es Obligatorio.")]
        [StringLength(50)]
        public string EvaluationType { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Calificación es Obligatoria.")]
        [Range(0, 100, ErrorMessage = "La Calificación debe estar entre 0 y 100.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "La Fecha es Obligatoria.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El Periodo es Obligatorio.")]
        [StringLength(20)]
        public string Period { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Observations { get; set; }

        // Propiedad calculada
        public bool IsApproved => Value >= 70;
    }
}
