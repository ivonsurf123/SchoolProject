using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data;


var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURACIÓN DE SERVICIOS =====

// 1. Agregar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// 2. Configurar autenticación con Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";           // Ruta de login
        options.LogoutPath = "/Auth/Logout";         // Ruta de logout
        options.AccessDeniedPath = "/Auth/AccessDenied"; // Acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromHours(8);  // Duración de la sesión
        options.SlidingExpiration = true;            // Renovar cookie automáticamente
        options.Cookie.HttpOnly = true;              // Seguridad: solo HTTP
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.Name = "SchoolManagementProject.Auth";
    });

// 3. Configurar políticas de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministrador", policy =>
        policy.RequireRole("Administrador"));

    options.AddPolicy("RequireProfesor", policy =>
        policy.RequireRole("Professor", "Administrador"));
});

// 4. Agregar soporte para Controllers con Views
builder.Services.AddControllersWithViews();

// 5. Agregar soporte para sesiones (opcional pero útil)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 6. Configurar servicios HTTP Context (para acceder al contexto en servicios)
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

