using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Models
{
    public class Professor
    {
        [Key]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "El Código es Obligatorio.")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage = "El Nombre es Obligatorio.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Apellido es Obligatorio.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{Name} {LastName}";

        [Required(ErrorMessage = "El Email es Obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email no es válido.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Teléfono es Obligatorio.")]
        [Phone(ErrorMessage = "El Teléfono no es válido.")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "La Especialidad es Obligatoria.")]
        [StringLength(100)]
        public string Department { get; set; }

        public DateTime HiringDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        //Foreign key to User 1:1 relationship
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //// Navigation property to Courses
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

