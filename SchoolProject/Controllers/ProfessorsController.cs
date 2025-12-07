namespace SchoolProject.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public ProfessorsController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // ==================== MÉTODO 1: CREAR (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfessor(ProfessorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Professors", "Dashboard");
            }

            var professor = new Professor
            {
                Code = model.Code,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Department = model.Department,
                HiringDate = model.HiringDate,
                IsActive = model.IsActive
            };

            _ctx.Professors.Add(professor);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Professors", "Dashboard");
        }

        // ==================== MÉTODO 2: ACTUALIZAR (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfessor(ProfessorViewModel model)
        {
            if (!ModelState.IsValid || model.ProfessorId == 0)
            {
                return RedirectToAction("Professors", "Dashboard");
            }

            var professor = await _ctx.Professors.FindAsync(model.ProfessorId);

            if (professor == null)
            {
                return NotFound();
            }

            // Actualizar solo campos editables (igual que en Students)
            professor.Phone = model.Phone;
            professor.Department = model.Department;
            professor.IsActive = model.IsActive;

            _ctx.Professors.Update(professor);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Professors", "Dashboard");
        }

        // ==================== MÉTODO 3: ELIMINAR (POST) ====================
        [HttpPost]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var professor = await _ctx.Professors.FindAsync(id);
            if (professor != null)
            {
                // Eliminación suave (cambiar estado a inactivo)
                professor.IsActive = false;
                await _ctx.SaveChangesAsync();
            }

            return RedirectToAction("Professors", "Dashboard");
        }

        // ==================== MÉTODO PRIVADO: OBTENER LISTA ====================
        private IQueryable<ProfessorViewModel> GetProfessorsList()
        {
            return _ctx.Professors
                .Where(p => p.IsActive)
                .Select(p => new ProfessorViewModel
                {
                    ProfessorId = p.ProfessorId,
                    Code = p.Code,
                    Name = p.Name,
                    LastName = p.LastName,
                    Email = p.Email,
                    Phone = p.Phone,
                    Department = p.Department,
                    HiringDate = p.HiringDate,
                    IsActive = p.IsActive,
                    UserId = p.UserId,
                    UserName = p.User != null ? p.User.UserName : null
                });
        }
    }
}
