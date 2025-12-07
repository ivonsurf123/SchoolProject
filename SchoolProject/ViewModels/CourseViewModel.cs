using System.ComponentModel.DataAnnotations;

namespace SchoolProject.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "El Código es Obligatorio.")]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Nombre del Curso es Obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Descripción es Obligatoria.")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los Créditos son Obligatorios.")]
        [Range(1, 10, ErrorMessage = "Los Créditos deben estar entre 1 y 10.")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "El Nivel es Obligatorio.")]
        [StringLength(50)]
        public string Level { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Grado es Obligatorio.")]
        [StringLength(20)]
        public string Grade { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // Relación con Profesor
        [Required(ErrorMessage = "Debe asignar un Profesor al Curso.")]
        public int ProfessorId { get; set; }
        public string? ProfessorName { get; set; }
    }
}

