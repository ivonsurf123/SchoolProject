using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace SchoolProject.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "El Código es Obligatorio.")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage = "El Estudiante es Obligatorio.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Apellido es Obligatorio.")]
        [StringLength(50)]
        public string Lastname { get; set; }

        [NotMapped]
        public string FullName => $"{Name} {Lastname}";

        [Required(ErrorMessage = "La Fecha de Nacimiento es Obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El Género es Obligatorio.")]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "La Dirección es Obligatoria.")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "El Teléfono es Obligatorio.")]
        [Phone(ErrorMessage = "El Teléfono no es válido.")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El Correo Electrónico es Obligatorio.")]
        [EmailAddress(ErrorMessage = "El Correo Electrónico no es válido.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Nombre del Tutor es Obligatorio.")]
        [StringLength(100)]
        public string GuardianName { get; set; }

        [Required(ErrorMessage = "El Teléfono del Tutor es Obligatorio.")]
        [Phone(ErrorMessage = "El Teléfono del Tutor no es válido.")]
        [StringLength(15)]
        public string GuardianPhone { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}

