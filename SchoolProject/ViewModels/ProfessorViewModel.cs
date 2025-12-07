namespace SchoolProject.ViewModels
{
    public class ProfessorViewModel
    {
        public int ProfessorId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{Name} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime HiringDate { get; set; }
        public bool IsActive { get; set; }

        // Relación opcional con User
        public int? UserId { get; set; }
        public string? UserName { get; set; }
    }
}

