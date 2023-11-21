using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using El_Muro.Models;

namespace El_Muro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var nombre = HttpContext.Session.GetString("Nombre");

        if (nombre != null)
        {
            return RedirectToAction("Mensajes");
        }
        else
        {
            return View();
        }
    }

    [HttpGet]
    [SessionCheck]
    public IActionResult Mensajes()
    {
        return View();
    }

    [HttpPost]
    [Route("usuario/login")]
    public IActionResult ProcesaLogin(Login login)
    {
        Console.WriteLine(ModelState.IsValid);
        if (ModelState.IsValid)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(usu => usu.Correo == login.Correo);

            if (usuario != null)
            {
                PasswordHasher<Login> Hasher = new();
                var result = Hasher.VerifyHashedPassword(login, usuario.Contrasena, login.Contrasena);

                if (result == PasswordVerificationResult.Success)
                {
                    // La contraseña es correcta
                    HttpContext.Session.SetString("Nombre", usuario.Nombre);
                    HttpContext.Session.SetString("Apellido", usuario.Apellido);
                    HttpContext.Session.SetString("Correo", usuario.Correo);
                    HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);

                    return RedirectToAction("Mensajes");
                }
                else
                {
                    // Contraseña incorrecta
                    ModelState.AddModelError(string.Empty, "Contraseña incorrecta");
                }
            }
            else
            {
                // Email no registrado
                ModelState.AddModelError("Correo", "El Correo no está registrado");
            }

            return View("Index");
        }

        return View("Index");
    }

    [HttpGet]
    [SessionCheck]
    public IActionResult Registro()
    {
        return View("Index");
    }

    [HttpPost]
    [Route("usuario/registro")]
    public IActionResult ProcesaRegistro(Usuario NuevoUsuario)
    {

        var existeUsuario = _context.Usuarios.FirstOrDefault(u => u.Correo == NuevoUsuario.Correo);

        if (existeUsuario != null)
        {
            ModelState.AddModelError("Email", "El correo ya está registrado.");
            return View("Index");
        }

        if (ModelState.IsValid)
        {
            PasswordHasher<Usuario> Hasher = new();
            NuevoUsuario.Contrasena = Hasher.HashPassword(NuevoUsuario, NuevoUsuario.Contrasena);
            _context.Usuarios.Add(NuevoUsuario);
            _context.SaveChanges();

            HttpContext.Session.SetString("Nombre", NuevoUsuario.Nombre);
            HttpContext.Session.SetString("Apellido", NuevoUsuario.Apellido);
            HttpContext.Session.SetString("Correo", NuevoUsuario.Correo);
            HttpContext.Session.SetInt32("UsuarioId", NuevoUsuario.UsuarioId);
            Console.WriteLine(NuevoUsuario.UsuarioId);
            return RedirectToAction("Mensajes");
        }
        return View("Index");
    }

    [HttpGet("Mensajes")]
    public IActionResult Muro()
    {
        if (HttpContext.Session.GetInt32("UsuarioId") == null)
        {
            return RedirectToAction("Index");
        }

        Usuario? thisUser = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == HttpContext.Session.GetInt32("UsuarioId"));
        ViewBag.ThisUser = thisUser;

        IEnumerable<Mensaje> ListaMensajes = _context.Mensajes
            .Include(u => u.Usuario)
            .Include(rc => rc.ListaComentarios)
            .ThenInclude(c => c.Usuario)
            .ToList().OrderByDescending(m => m.Fecha_Creacion);
            Console.WriteLine("ACATAA");
        ViewBag.AllMensajes = ListaMensajes;

        return View("Mensajes");
    }

    [HttpPost("posteo")]
    public IActionResult PostearMensaje(Mensaje mensaje)
    {
        Mensaje NuevoMensaje = new Mensaje
        {
            UsuarioId = (int)HttpContext.Session.GetInt32("UsuarioId")!,
            MensajeTexto = mensaje.MensajeTexto
        };
        _context.Add(NuevoMensaje);
        _context.SaveChanges();
        return RedirectToAction("Mensajes");
    }

    [HttpGet]
    [Route("Usuario/ProcesaLogout")]
    public IActionResult ProcesaLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        string? Correo = context.HttpContext.Session.GetString("Correo");
        // Check to see if we got back null
        if (Correo == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "", null);
        }
    }
}
