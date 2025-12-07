namespace SchoolProject.Controllers
{
    public class ClassSchedulesController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public ClassSchedulesController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // ==================== MÉTODO 1: CREAR HORARIO (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClassSchedule(ClassScheduleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ClassSchedules", "Dashboard");
            }

            // Validar que la hora de fin sea mayor que la hora de inicio
            if (model.EndTime <= model.StartTime)
            {
                TempData["Error"] = "La hora de fin debe ser mayor que la hora de inicio.";
                return RedirectToAction("ClassSchedules", "Dashboard");
            }

            // Validar conflictos de horario (mismo aula, mismo día, mismo rango de horas)
            var conflict = await _ctx.ClassSchedules
                .Where(cs => cs.Classroom == model.Classroom
                          && cs.Day == model.Day
                          && cs.IsActive
                          && ((model.StartTime >= cs.StartTime && model.StartTime < cs.EndTime) ||
                              (model.EndTime > cs.StartTime && model.EndTime <= cs.EndTime) ||
                              (model.StartTime <= cs.StartTime && model.EndTime >= cs.EndTime)))
                .FirstOrDefaultAsync();

            if (conflict != null)
            {
                TempData["Error"] = $"Conflicto de horario: El aula {model.Classroom} ya está ocupada en ese horario.";
                return RedirectToAction("ClassSchedules", "Dashboard");
            }

            var schedule = new ClassSchedule
            {
                CourseId = model.CourseId,
                Day = model.Day,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Classroom = model.Classroom,
                Period = model.Period,
                IsActive = true
            };

            _ctx.ClassSchedules.Add(schedule);
            await _ctx.SaveChangesAsync();

            TempData["Success"] = "Horario creado exitosamente.";
            return RedirectToAction("ClassSchedules", "Dashboard");
        }

        // ==================== MÉTODO 2: ACTUALIZAR HORARIO (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateClassSchedule(ClassScheduleViewModel model)
        {
            if (!ModelState.IsValid || model.ClassScheduleId == 0)
            {
                return RedirectToAction("ClassSchedules", "Dashboard");
            }

            var schedule = await _ctx.ClassSchedules.FindAsync(model.ClassScheduleId);

            if (schedule == null)
            {
                return NotFound();
            }

            // Validar que la hora de fin sea mayor que la hora de inicio
            if (model.EndTime <= model.StartTime)
            {
                TempData["Error"] = "La hora de fin debe ser mayor que la hora de inicio.";
                return RedirectToAction("ClassSchedules", "Dashboard");
            }

            // Actualizar campos editables
            schedule.Day = model.Day;
            schedule.StartTime = model.StartTime;
            schedule.EndTime = model.EndTime;
            schedule.Classroom = model.Classroom;
            schedule.Period = model.Period;
            schedule.IsActive = model.IsActive;

            _ctx.ClassSchedules.Update(schedule);
            await _ctx.SaveChangesAsync();

            TempData["Success"] = "Horario actualizado exitosamente.";
            return RedirectToAction("ClassSchedules", "Dashboard");
        }

        // ==================== MÉTODO 3: ELIMINAR HORARIO (POST) ====================
        [HttpPost]
        public async Task<IActionResult> DeleteClassSchedule(int id)
        {
            var schedule = await _ctx.ClassSchedules.FindAsync(id);
            if (schedule != null)
            {
                schedule.IsActive = false;
                await _ctx.SaveChangesAsync();
                TempData["Success"] = "Horario eliminado exitosamente.";
            }

            return RedirectToAction("ClassSchedules", "Dashboard");
        }
    }
}
