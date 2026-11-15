using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSCORP.Web.Data;
using OSCORP.Web.Models;
using OSCORP.Web.Services;
using OSCORP.Web.ViewModels;

namespace OSCORP.Web.Controllers;

public class AccountController(OscorpDbContext db, PasswordService passwords) : Controller
{
    [AllowAnonymous]
    public IActionResult Login() => View();

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await db.Usuarios.SingleOrDefaultAsync(u => u.Correo == model.Correo);
        if (user is null || !passwords.Verify(user.Contrasena, model.Contrasena, out var upgrade))
        {
            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View(model);
        }

        if (upgrade)
        {
            user.Contrasena = passwords.Hash(model.Contrasena);
            await db.SaveChangesAsync();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
            new(ClaimTypes.Name, user.Nombre),
            new(ClaimTypes.Email, user.Correo),
            new(ClaimTypes.Role, user.TipoUsuario)
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

        return user.TipoUsuario switch
        {
            "Administrador" => RedirectToAction("Index", "Admin"),
            "Doctor" => RedirectToAction("Index", "Doctor"),
            _ => RedirectToAction("Index", "Appointments")
        };
    }

    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        if (await db.Usuarios.AnyAsync(u => u.Correo == model.Correo))
        {
            ModelState.AddModelError(nameof(model.Correo), "Ese correo ya está registrado.");
            return View(model);
        }

        var user = new Usuario
        {
            Nombre = model.Nombre.Trim(),
            Correo = model.Correo.Trim().ToLowerInvariant(),
            Contrasena = passwords.Hash(model.Contrasena),
            TipoUsuario = "Paciente"
        };
        db.Usuarios.Add(user);
        await db.SaveChangesAsync();
        db.Pacientes.Add(new Paciente { IdPaciente = user.IdUsuario });
        await db.SaveChangesAsync();

        TempData["Mensaje"] = "Cuenta creada. Ahora puedes iniciar sesión.";
        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();
}
