using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is Mandatory.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be 3 characters min. and 50 characters max.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FullName is Mandatory.")]
        [StringLength(100, ErrorMessage = "FullName must be no more than 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email si Mandatory.")]
        [EmailAddress(ErrorMessage = "El Correo Electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El Correo Electrónico no debe exceder los 100 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Mandatory.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La Contraseña debe tener entre 6 y 100 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is Mandatory.")]
        [StringLength(20, ErrorMessage = "El Rol no debe exceder los 20 caracteres.")]
        public string Role { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastAccess { get; set; }

        // Navigation property to Professor
        public virtual Professor? Professor { get; set; }
    }
}

