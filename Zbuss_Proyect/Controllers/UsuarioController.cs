using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Zbuss_Proyect.Models;
using Zbuss_Proyect.ViewModel;

using System.Net.Mail;
using System.Net;

namespace Zbuss_Proyect.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext bd;

        private UserManager<IdentityUser> _userManager;


        public UsuarioController(bd_VENTAS_ZBUSSContext context)
        {
            bd = context;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public TbUsuarios Create(TbUsuarios pUser)
        {
            if (!TbUsuariosExists(pUser.Correo))
            {
                if (!TbUsuariosDniExists(pUser.NroDocumento))
                {
                    TbUsuarios userBd = new TbUsuarios()
                    {
                        TipoDoc = pUser.TipoDoc,
                        NroDocumento = pUser.NroDocumento,
                        Nombres = pUser.Nombres,
                        Apellidos = pUser.Apellidos,
                        Correo = pUser.Correo,
                        Contrasena = pUser.Contrasena,
                        Celular = pUser.Celular,
                        Rol = 1,
                        Estado = true
                    };
                    if (ModelState.IsValid)
                    {
                        bd.TbUsuarios.Add(userBd);
                        bd.SaveChanges();

                        pUser.IdUsuario = userBd.IdUsuario;

                        return userBd;
                    }
                }
                ViewBag.Alert = "El documento ingresado ya está asociado a una cuenta. Pruebe con otro";
                return null;
            }
            ViewBag.Alert = "El correo ingresado ya está asociado a una cuenta. Pruebe con otro";
            return null;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public UsuarioLogin user;

        [HttpPost]
        public IActionResult Login(UsuarioLogin usuario)
        {
            ViewData["mensaje"] = "";
            if (string.IsNullOrEmpty(usuario.Correo) && string.IsNullOrEmpty(usuario.Contrasena))
            {
                ViewData["mensaje"] = "No ingreso correo y/o contraseña";
                return View();
            }

            var userBd = bd.TbUsuarios.FirstOrDefault(x => x.Correo == usuario.Correo && x.Contrasena == usuario.Contrasena);
            if (userBd == null)
            {
                ViewData["mensaje"] = "Error correo y/o contraseña no válidas";
                return View();
            }
            if (userBd.Reestablecer == true)
            {
                TempData["idUser"] = userBd.IdUsuario;
                return RedirectToAction("ConfigurarContrasena");
            }
            else
            {
                if (userBd.Estado == false)
                {
                    ViewData["mensaje"] = "Cuenta deshabilitada. Pruebe otro o contáctese con un técnico para su reactivación";
                }
                else
                {
                    if (bd.TbUsuarios.Count(x => (x.Contrasena == usuario.Contrasena && x.Correo == usuario.Correo)) > 0)
                    {
                        if(userBd.Rol == 1)
                        {
                            var identity = new ClaimsIdentity(new[]{
                            new Claim(ClaimTypes.Name, usuario.Correo),
                            new Claim(ClaimTypes.Role,"GENERAL")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        }
                        else if(userBd.Rol == 4)
                        {
                            var identity = new ClaimsIdentity(new[]{
                            new Claim(ClaimTypes.Name, usuario.Correo),
                            new Claim(ClaimTypes.Role,"ADMIN")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["mensaje"] = "Error correo y/o contraseña no válidas";
                    }
                }
            }

            return View();
        }

        public string GeneratePassword()
        {
            int PasswordLength = 6;
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public string nuevaClave;

        public bool RestablecerClave(int id)
        {
            TbUsuarios userBd = new TbUsuarios();

            userBd = bd.TbUsuarios.FirstOrDefault(x => x.IdUsuario == id);

            if (userBd == null)
            {
                return false;
            }

            nuevaClave = GeneratePassword();

            userBd.Contrasena = nuevaClave;
            userBd.Reestablecer = true;

            //string body =
            //    "<body>"+
            //        "<h1>Mensaje para reestablecer tu contraseña</h1>" +
            //        "<h4>Querido usuario </h4>" +
            //        "<span> Debido a que ha olvidado su contraseña, a continuación le ofreceremos una contraseña provisional con la que podrá iniciar sesión y posteriormente cambiar su contraseña. </span>" +
            //        "<span> Su contraseña provisional es:</span>" +
            //        "<h3>"+ nuevaClave +"</h3>" +
            //        "<span> Te agradece Zbuss Sac por seguir usando nuestra plataforma. Buen dia!</span>" +
            //   "</body>";

            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //smtp.Credentials = new NetworkCredential("zbuss.business@gmail.com", "pkywhbrllbykviae");
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.EnableSsl = true;
            //smtp.UseDefaultCredentials = false;

            //MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("zbuss.business@gmail.com", "Reestablecer Contraseña");
            //mail.To.Add(new MailAddress(correo));
            //mail.Subject = "Reestablecer Contraseña";
            //mail.IsBodyHtml = true;
            //mail.Body = body;

            //smtp.Send(mail);

            bd.Entry(userBd).State = EntityState.Modified;
            bd.SaveChanges();
            return true;

        }
        public ActionResult ConfigurarContrasena()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfigurarContrasena(string contrasena, string celular)
        {
            if(contrasena != celular)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            var IdUser = Convert.ToInt32(TempData["idUser"]);

            TbUsuarios userBd = bd.TbUsuarios.FirstOrDefault(x => x.IdUsuario == IdUser);
            if (userBd == null)
            {
                ViewBag.Error = "No se encontró usuario";
            }
            userBd.Contrasena = contrasena;
            userBd.Reestablecer = false;
            bd.Entry(userBd).State = EntityState.Modified;
            bd.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Restablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Restablecer(string correo, string celular)
        {
            TbUsuarios oUser = new TbUsuarios();

            oUser = bd.TbUsuarios.FirstOrDefault(x => x.Correo == correo);

            if (oUser == null)
            {
                ViewBag.Error = "No se encontró usuario asociado a este correo";
                return View();
            }

            if (celular != oUser.Celular)
            {
                ViewBag.Error = "Teléfono incorrecto, pruebe con otro teléfono o correo";
                return View();
            }

            string mensaje = string.Empty;
            bool respuesta = RestablecerClave(oUser.IdUsuario);

            if (respuesta)
            {
                ViewBag.Error = "Su nueva contraseña provicional es "+ nuevaClave;
                return View();
                //return RedirectToAction("Login", "Usuario");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["mensaje"] = "Sesión cerrada. Vuelva a iniciar sesión";
            return RedirectToAction("Login", "Usuario");
        }

        private bool TbUsuariosExists(string correo)
        {
            return bd.TbUsuarios.Any(e => e.Correo == correo);
        }

        private bool TbUsuariosDniExists(string nroDocumento)
        {
            return bd.TbUsuarios.Any(e => e.NroDocumento == nroDocumento);
        }
    }
}
