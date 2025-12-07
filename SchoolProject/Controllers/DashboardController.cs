namespace SchoolProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public DashboardController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        [HttpGet]
        public IActionResult Dashboard(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;    
            var totalStudents = GetTotalStudents();
            ViewBag.TotalStudents = totalStudents;
            var totalProfessors = GetTotalProfessors();
            ViewBag.TotalProfessors = totalProfessors;
            var totalCourses = GetTotalCourses();
            ViewBag.TotalCourses = totalCourses;
            var totalEnrollments = GetTotalEnrollments();
            ViewBag.TotalEnrollments = totalEnrollments;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Students(int? pageNumber)
        {
            int pageSize = 8; 
            var students = GetStudentsList();
            var paginatedList = await PaginatedList<StudentViewModel>.CreateAsync(
            students.AsNoTracking(), // Es mejor usar AsNoTracking para consultas de solo lectura
            pageNumber ?? 1,
            pageSize
            );

            return View("Students", paginatedList);
        }

        private IQueryable<StudentViewModel> GetStudentsList()
        {
            return _ctx.Students
                .Where(s => s.IsActive)
                .Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    Code = s.Code,
                    Name = s.Name,
                    Lastname = s.Lastname,
                    BirthDate = s.BirthDate,
                    Gender = s.Gender,
                    Address = s.Address,
                    Phone = s.Phone,
                    Email = s.Email,
                    GuardianName = s.GuardianName,
                    GuardianPhone = s.GuardianPhone,
                    EnrollmentDate = s.EnrollmentDate,
                    IsActive = s.IsActive
                });
        }


        // ==================== PROFESORES ====================
        [HttpGet]
        public async Task<IActionResult> Professors(int? pageNumber)
        {
            int pageSize = 8;
            var professors = GetProfessorsList();
            var paginatedList = await PaginatedList<ProfessorViewModel>.CreateAsync(
                professors.AsNoTracking(),
                pageNumber ?? 1,
                pageSize
            );

            return View("Professors", paginatedList);
        }

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
                    IsActive = p.IsActive
                });
        }

        // ==================== CURSOS ====================
        [HttpGet]
        public async Task<IActionResult> Courses(int? pageNumber)
        {
            int pageSize = 8;
            var courses = GetCoursesList();
            var paginatedList = await PaginatedList<CourseViewModel>.CreateAsync(
                courses.AsNoTracking(),
                pageNumber ?? 1,
                pageSize
            );

            // Pasar lista de profesores activos para el selector
            ViewBag.Professors = await _ctx.Professors
                .Where(p => p.IsActive)
                .Select(p => new { p.ProfessorId, FullName = p.Name + " " + p.LastName })
                .ToListAsync();

            return View("Courses", paginatedList);
        }

        private IQueryable<CourseViewModel> GetCoursesList()
        {
            return _ctx.Courses
                .Where(c => c.IsActive)
                .Include(c => c.Professor)
                .Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    Code = c.Code,
                    Name = c.Name,
                    Description = c.Description,
                    Credits = c.Credits,
                    Level = c.Level,
                    Grade = c.Grade,
                    IsActive = c.IsActive,
                    ProfessorId = c.ProfessorId,
                    ProfessorName = c.Professor != null ? c.Professor.Name + " " + c.Professor.LastName : "Sin asignar"
                });
        }

        // ==================== INSCRIPCIONES ====================
        [HttpGet]
        public async Task<IActionResult> Enrollments(int? pageNumber)
        {
            int pageSize = 10;
            var enrollments = GetEnrollmentsList();
            var paginatedList = await PaginatedList<EnrollmentViewModel>.CreateAsync(
                enrollments.AsNoTracking(),
                pageNumber ?? 1,
                pageSize
            );

            // Pasar lista de estudiantes activos
            ViewBag.Students = await _ctx.Students
                .Where(s => s.IsActive)
                .Select(s => new { s.StudentId, FullName = s.Name + " " + s.Lastname, s.Code })
                .ToListAsync();

            // Pasar lista de cursos activos
            ViewBag.Courses = await _ctx.Courses
                .Where(c => c.IsActive)
                .Select(c => new { c.CourseId, c.Name, c.Code })
                .ToListAsync();

            return View("Enrollments", paginatedList);
        }

        private IQueryable<EnrollmentViewModel> GetEnrollmentsList()
        {
            return _ctx.Enrollments
                .Where(e => e.IsActive)
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new EnrollmentViewModel
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentId = e.StudentId,
                    StudentCode = e.Student != null ? e.Student.Code : "N/A",
                    StudentName = e.Student != null ? e.Student.Name + " " + e.Student.Lastname : "Sin asignar",
                    CourseId = e.CourseId,
                    CourseCode = e.Course != null ? e.Course.Code : "N/A",
                    CourseName = e.Course != null ? e.Course.Name : "Sin asignar",
                    Period = e.Period,
                    EnrollmentDate = e.EnrollmentDate,
                    IsActive = e.IsActive
                })
                .OrderByDescending(e => e.EnrollmentDate);
        }

        // ==================== HORARIOS ====================
        [HttpGet]
        public async Task<IActionResult> ClassSchedules(int? pageNumber)
        {
            int pageSize = 10;
            var schedules = GetClassSchedulesList();
            var paginatedList = await PaginatedList<ClassScheduleViewModel>.CreateAsync(
                schedules.AsNoTracking(),
                pageNumber ?? 1,
                pageSize
            );

            // Pasar lista de cursos activos con información del profesor
            ViewBag.Courses = await _ctx.Courses
                .Where(c => c.IsActive)
                .Include(c => c.Professor)
                .Select(c => new {
                    c.CourseId,
                    c.Name,
                    c.Code,
                    ProfessorName = c.Professor != null ? c.Professor.Name + " " + c.Professor.LastName : "Sin profesor"
                })
                .ToListAsync();

            return View("ClassSchedules", paginatedList);
        }

        private IQueryable<ClassScheduleViewModel> GetClassSchedulesList()
        {
            return _ctx.ClassSchedules
                .Where(cs => cs.IsActive)
                .Include(cs => cs.Course)
                    .ThenInclude(c => c.Professor)
                .Select(cs => new ClassScheduleViewModel
                {
                    ClassScheduleId = cs.ClassScheduleId,
                    CourseId = cs.CourseId,
                    CourseCode = cs.Course != null ? cs.Course.Code : "N/A",
                    CourseName = cs.Course != null ? cs.Course.Name : "Sin asignar",
                    ProfessorName = cs.Course != null && cs.Course.Professor != null
                        ? cs.Course.Professor.Name + " " + cs.Course.Professor.LastName
                        : "Sin profesor",
                    Day = cs.Day,
                    StartTime = cs.StartTime,
                    EndTime = cs.EndTime,
                    Classroom = cs.Classroom,
                    Period = cs.Period,
                    IsActive = cs.IsActive
                })
                .OrderBy(cs => cs.Day)
                    .ThenBy(cs => cs.StartTime);
        }

        // ==================== CALIFICACIONES ====================
        [HttpGet]
        public async Task<IActionResult> Scores(int? pageNumber)
        {
            int pageSize = 10;
            var scores = GetScoresList();
            var paginatedList = await PaginatedList<ScoreViewModel>.CreateAsync(
                scores.AsNoTracking(),
                pageNumber ?? 1,
                pageSize
            );
            
            // Pasar lista de estudiantes activos
            ViewBag.Students = await _ctx.Students
                .Where(s => s.IsActive)
                .Select(s => new { s.StudentId, FullName = s.Name + " " + s.Lastname, s.Code })
                .ToListAsync();

            // Pasar lista de cursos activos
            ViewBag.Courses = await _ctx.Courses
                .Where(c => c.IsActive)
                .Select(c => new { c.CourseId, c.Name, c.Code })
                .ToListAsync();

            return View("Scores", paginatedList);
        }

        private IQueryable<ScoreViewModel> GetScoresList()
        {
            return _ctx.Scores
                .Include(s => s.Student)
                .Include(s => s.Course)
                .Select(s => new ScoreViewModel
                {
                    ScoreId = s.ScoreId,
                    StudentId = s.StudentId,
                    StudentCode = s.Student != null ? s.Student.Code : "N/A",
                    StudentName = s.Student != null ? s.Student.Name + " " + s.Student.Lastname : "Sin asignar",
                    CourseId = s.CourseId,
                    CourseCode = s.Course != null ? s.Course.Code : "N/A",
                    CourseName = s.Course != null ? s.Course.Name : "Sin asignar",
                    EvaluationType = s.EvaluationType,
                    Value = s.Value,
                    Date = s.Date,
                    Period = s.Period,
                    Observations = s.Observations
                })
                .OrderByDescending(s => s.Date);
        }

        public int GetTotalStudents()
        {
            return _ctx.Students.Count();
        }

        public int GetTotalProfessors()
        {
            return _ctx.Professors.Count(p => p.IsActive);
        }

        public int GetTotalCourses()
        {
            return _ctx.Courses.Count(c => c.IsActive);
        }

        public int GetTotalEnrollments()
        {
            return _ctx.Enrollments.Count(e => e.IsActive);
        }

    }
}
