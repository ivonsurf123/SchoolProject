namespace SchoolProject.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public EnrollmentsController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // ==================== MÉTODO 1: CREAR INSCRIPCIÓN (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnrollment(EnrollmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Enrollments", "Dashboard");
            }

            // Validar que no exista ya una inscripción activa para este estudiante en este curso
            var existingEnrollment = await _ctx.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == model.StudentId
                                       && e.CourseId == model.CourseId
                                       && e.IsActive);

            if (existingEnrollment != null)
            {
                // Si ya existe, redirigir con mensaje de error (puedes usar TempData)
                TempData["Error"] = "El estudiante ya está inscrito en este curso.";
                return RedirectToAction("Enrollments", "Dashboard");
            }

            var enrollment = new Enrollment
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                Period = model.Period,
                EnrollmentDate = DateTime.UtcNow,
                IsActive = true
            };

            _ctx.Enrollments.Add(enrollment);
            await _ctx.SaveChangesAsync();

            TempData["Success"] = "Inscripción realizada exitosamente.";
            return RedirectToAction("Enrollments", "Dashboard");
        }

        // ==================== MÉTODO 2: ACTUALIZAR INSCRIPCIÓN (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEnrollment(EnrollmentViewModel model)
        {
            if (!ModelState.IsValid || model.EnrollmentId == 0)
            {
                return RedirectToAction("Enrollments", "Dashboard");
            }

            var enrollment = await _ctx.Enrollments.FindAsync(model.EnrollmentId);

            if (enrollment == null)
            {
                return NotFound();
            }

            // Actualizar solo el periodo y estado
            enrollment.Period = model.Period;
            enrollment.IsActive = model.IsActive;

            _ctx.Enrollments.Update(enrollment);
            await _ctx.SaveChangesAsync();

            TempData["Success"] = "Inscripción actualizada exitosamente.";
            return RedirectToAction("Enrollments", "Dashboard");
        }

        // ==================== MÉTODO 3: CANCELAR INSCRIPCIÓN (POST) ====================
        [HttpPost]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _ctx.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                // Cancelar inscripción (cambiar estado a inactivo)
                enrollment.IsActive = false;
                await _ctx.SaveChangesAsync();
                TempData["Success"] = "Inscripción cancelada exitosamente.";
            }

            return RedirectToAction("Enrollments", "Dashboard");
        }



    }
}
