using System.ComponentModel.DataAnnotations;

namespace SchoolProject.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(20, ErrorMessage = "El código no puede exceder 20 caracteres")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }        // <-- nullable

        [Required(ErrorMessage = "El género es requerido")]
        [StringLength(10, ErrorMessage = "El género no puede exceder 10 caracteres")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        [RegularExpression(@"^[0-9\-\+\s\(\)]+$", ErrorMessage = "Formato de teléfono inválido")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre del tutor es requerido")]
        [StringLength(100, ErrorMessage = "El nombre del tutor no puede exceder 100 caracteres")]
        [Display(Name = "Nombre del Tutor")]
        public string GuardianName { get; set; }

        [Required(ErrorMessage = "El teléfono del tutor es requerido")]
        [StringLength(20, ErrorMessage = "El teléfono del tutor no puede exceder 20 caracteres")]
        [RegularExpression(@"^[0-9\-\+\s\(\)]+$", ErrorMessage = "Formato de teléfono inválido")]
        [Display(Name = "Teléfono del Tutor")]
        public string GuardianPhone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Matrícula")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; }
    }
}
