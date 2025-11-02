using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;
using System.Security.Claims;

namespace SchoolProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public AuthController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Hashear la contraseña para compararla
            var hashedPassword = HashPassword(model.Password);

            // Buscar usuario en la base de datos
            var usuario = await _ctx.Users
                .Include(u => u.Professor)
                .FirstOrDefaultAsync(u =>
                    u.UserName == model.UserName &&
                    u.Password == hashedPassword &&
                    u.IsActive);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return View(model);
            }

            // Actualizar último acceso
            usuario.LastAccess = DateTime.Now;
            await _ctx.SaveChangesAsync();

            // Crear claims (información del usuario en la sesión)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UserId.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role),
                new Claim("FullName", usuario.FullName)
            };

            // Si es profesor, agregar el ProfesorId
            if (usuario.Professor != null)
            {
                claims.Add(new Claim("ProfessorId", usuario.Professor.ProfessorId.ToString()));
            }

            // Crear identidad
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Propiedades de autenticación
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RemenberMe,
                ExpiresUtc = model.RemenberMe ?
                    DateTimeOffset.UtcNow.AddDays(30) :
                    DateTimeOffset.UtcNow.AddHours(8)
            };

            // Iniciar sesión
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Redirigir
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// GET: /Auth/Register
        /// Muestra el formulario de registro
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// POST: /Auth/Register
        /// Procesa el registro de un nuevo usuario
        /// Si el usuario desea, también crea su perfil de profesor
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validaciones de duplicados para Usuario
            var usuarioExiste = await _ctx.Users
                .AnyAsync(u => u.UserName == model.UserName);

            if (usuarioExiste)
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya existe");
                return View(model);
            }

            var emailUsuarioExiste = await _ctx.Users
                .AnyAsync(u => u.Email == model.Email);

            if (emailUsuarioExiste)
            {
                ModelState.AddModelError("Email", "El email ya está registrado como usuario");
                return View(model);
            }

            // Validaciones adicionales si se está creando perfil de profesor
            if (model.CreateProfessorProfile)
            {
                var codigoProfesorExiste = await _ctx.Profesors
                    .AnyAsync(p => p.Code == model.ProfessorCode);

                if (codigoProfesorExiste)
                {
                    ModelState.AddModelError("CodigoProfesor", "El código de profesor ya existe");
                    return View(model);
                }

                var emailProfesorExiste = await _ctx.Profesors
                    .AnyAsync(p => p.Email == model.Email);

                if (emailProfesorExiste)
                {
                    ModelState.AddModelError("Email", "El email ya está registrado como profesor");
                    return View(model);
                }
            }

            try
            {
                // 1. Crear el usuario
                var nuevoUsuario = new User
                {
                    UserName = model.UserName,
                    Password = HashPassword(model.Password),
                    FullName = model.FullName,
                    Email = model.Email,
                    Role = "Profesor",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _ctx.Users.Add(nuevoUsuario);
                await _ctx.SaveChangesAsync();

                // 2. Si desea crear perfil de profesor, crearlo y vincularlo
                if (model.CreateProfessorProfile)
                {
                    var nuevoProfesor = new Professor
                    {
                        Code = model.ProfessorCode,
                        Name = model.Name,
                        LastName = model.Lastname,
                        Email = model.Email,
                        Phone = model.Phone,
                        Department = model.Department,
                        HiringDate = DateTime.Now,
                        IsActive = true,
                        UserId = nuevoUsuario.UserId
                    };

                    _ctx.Profesors.Add(nuevoProfesor);
                    await _ctx.SaveChangesAsync();
                }

                TempData["Mensaje"] = model.CreateProfessorProfile
                    ? "Registro exitoso. Tu usuario y perfil de profesor han sido creados. Por favor inicia sesión."
                    : "Registro exitoso. Por favor inicia sesión.";
                TempData["TipoMensaje"] = "success";

                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al registrar: " + ex.Message);
                return View(model);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var HashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(HashBytes);
            }
        }

    }
}
