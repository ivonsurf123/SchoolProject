using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "El Código es Obligatorio.")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage = "El Nombre del Curso es Obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "La Descripción es Obligatoria.")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Los Créditos son Obligatorios.")]
        [Range(1, 10, ErrorMessage = "Los Créditos deben estar entre 1 y 10.")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "El Nivel es Obligatorio.")]
        [StringLength(50)]
        public string Level { get; set; }

        [Required(ErrorMessage = "El Grado es Obligatorio.")]
        [StringLength(50)]
        public string Grade { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign key to Professor
        public int ProfessorId { get; set; }
        [ForeignKey("ProfessorId")]
        public virtual Professor? Professor { get; set; }

        //// Navigation property to Enrollments
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
    }
}

