namespace SchoolProject.Controllers
{
    public class ScoresController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public ScoresController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // ==================== MÉTODO 1: CREAR CALIFICACIÓN (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScore(ScoreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Scores", "Dashboard");
            }

            // Validar que el estudiante esté inscrito en el curso
            var enrollment = await _ctx.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == model.StudentId
                                       && e.CourseId == model.CourseId
                                       && e.IsActive);

            if (enrollment == null)
            {
                TempData["Error"] = "El estudiante no está inscrito en este curso.";
                return RedirectToAction("Scores", "Dashboard");
            }

            var score = new Score
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                EvaluationType = model.EvaluationType,
                Value = model.Value,
                Date = model.Date,
                Period = model.Period,
                Observations = model.Observations
            };

            _ctx.Scores.Add(score);
            await _ctx.SaveChangesAsync();

            TempData["Success"] = "Calificación registrada exitosamente.";
            return RedirectToAction("Scores", "Dashboard");
        }

        // ==================== MÉTODO 2: ACTUALIZAR CALIFICACIÓN (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateScore(ScoreViewModel model)
        {
            if (!ModelState.IsValid || model.ScoreId == 0)
            {
                return RedirectToAction("Scores", "Dashboard");
            }

            var score = await _ctx.Scores.FindAsync(model.ScoreId);

            if (score == null)
            {
                return NotFound();
            }

            // Actualizar campos editables
            score.EvaluationType = model.EvaluationType;
            score.Value = model.Value;
            score.Date = model.Date;
            score.Period = model.Period;
            score.Observations = model.Observations;

            _ctx.Scores.Update(score);
            await _ctx.SaveChangesAsync();

            TempData["Success"] = "Calificación actualizada exitosamente.";
            return RedirectToAction("Scores", "Dashboard");
        }

        // ==================== MÉTODO 3: ELIMINAR CALIFICACIÓN (POST) ====================
        [HttpPost]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var score = await _ctx.Scores.FindAsync(id);
            if (score != null)
            {
                // Eliminar físicamente (no hay campo IsActive en Score)
                _ctx.Scores.Remove(score);
                await _ctx.SaveChangesAsync();
                TempData["Success"] = "Calificación eliminada exitosamente.";
            }

            return RedirectToAction("Scores", "Dashboard");
        }




    }
}
