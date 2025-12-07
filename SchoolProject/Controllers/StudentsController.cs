namespace SchoolProject.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public StudentsController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Recomendado para formularios POST
        public async Task<IActionResult> CreateStudent(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }

            var student = new Student
            {
                Code = model.Code,
                Name = model.Name,
                Lastname = model.Lastname,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                GuardianName = model.GuardianName,
                GuardianPhone = model.GuardianPhone,
                EnrollmentDate = model.EnrollmentDate,
                IsActive = model.IsActive
            };

            _ctx.Students.Add(student);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Students", "Dashboard");
        }

        // --- MÉTODO 2: ACTUALIZAR (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken] // Recomendado para formularios POST
        public async Task<IActionResult> UpdateStudent(StudentViewModel model)
        {
            if (!ModelState.IsValid || model.StudentId == 0)
            {
                // Si falla la validación o no hay ID, redirigir
                return RedirectToAction("Students", "Dashboard");
            }

            var student = await _ctx.Students.FindAsync(model.StudentId);

            if (student == null)
            {
                return NotFound(); // Retornar 404 si no se encuentra
            }

            // Actualizar propiedades
            student.Code = model.Code;
            student.Name = model.Name;
            student.Lastname = model.Lastname;
            student.BirthDate = model.BirthDate;
            student.Gender = model.Gender;
            student.Address = model.Address;
            student.Phone = model.Phone;
            student.Email = model.Email;
            student.GuardianName = model.GuardianName;
            student.GuardianPhone = model.GuardianPhone;
            student.EnrollmentDate = model.EnrollmentDate;
            student.IsActive = model.IsActive; // Actualización correcta

            _ctx.Students.Update(student);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Students", "Dashboard"); // Redirige a la lista
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _ctx.Students.FindAsync(id);
            if (student != null)
            {
                // Eliminación suave (cambiar estado a inactivo)
                student.IsActive = false;
                await _ctx.SaveChangesAsync();
            }

            return RedirectToAction("Students", "Dashboard");
        }


    }
}
