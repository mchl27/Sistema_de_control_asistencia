using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_asistencia.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Sistema_de_asistencia.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbSistemaControlAccesoContext _context;

        public HomeController(DbSistemaControlAccesoContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string nombreUsuario, string contrasena)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Nombre == nombreUsuario && u.Contrasena == contrasena);

            if (usuario != null)
            {

                if (usuario.Rol == "Inactivo")
                {
                    ModelState.AddModelError("", "Usuario inhabilitado.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Main");
            }
            else
            {
                // Nombre de usuario o contraseña incorrectos.
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
