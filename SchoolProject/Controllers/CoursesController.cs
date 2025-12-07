namespace SchoolProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public CoursesController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // ==================== MÉTODO 1: CREAR (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Courses", "Dashboard");
            }

            var course = new Course
            {
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                Credits = model.Credits,
                Level = model.Level,
                Grade = model.Grade,
                ProfessorId = model.ProfessorId,
                IsActive = model.IsActive
            };

            _ctx.Courses.Add(course);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Courses", "Dashboard");
        }

        // ==================== MÉTODO 2: ACTUALIZAR (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid || model.CourseId == 0)
            {
                return RedirectToAction("Courses", "Dashboard");
            }

            var course = await _ctx.Courses.FindAsync(model.CourseId);

            if (course == null)
            {
                return NotFound();
            }

            // Actualizar campos editables
            course.Description = model.Description;
            course.Credits = model.Credits;
            course.Level = model.Level;
            course.Grade = model.Grade;
            course.ProfessorId = model.ProfessorId;
            course.IsActive = model.IsActive;

            _ctx.Courses.Update(course);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Courses", "Dashboard");
        }

        // ==================== MÉTODO 3: ELIMINAR (POST) ====================
        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _ctx.Courses.FindAsync(id);
            if (course != null)
            {
                // Eliminación suave (cambiar estado a inactivo)
                course.IsActive = false;
                await _ctx.SaveChangesAsync();
            }

            return RedirectToAction("Courses", "Dashboard");
        }


    }
}
