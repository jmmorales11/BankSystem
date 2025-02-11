using Entities;
using Proxy;
using Service.Models;
using SL.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IEmailService _emailService;

        public UserController()
        {
            _emailService = new EmailService(); // Inicialización manual del servicio de correos
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }


        //enviar el codigo de verificacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User newUser)
        {
            var proxy_service = new ProxyUser();

            if (ModelState.IsValid)
            {
                try
                {
                    var createdUserResponse = proxy_service.Create(newUser);

                    if (createdUserResponse != null && createdUserResponse.Message != null)
                    {
                        TempData["SuccessMessage"] = createdUserResponse.Message;
                        return RedirectToAction("VerifyAndCreateUser");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return View(newUser);
        }



        public ActionResult VerifyAndCreateUser()
        {
            return View();
        }

        // Verificar el código y crear el usuario
        [HttpPost]
        public async Task<ActionResult> VerifyAndCreateUser(string email, string code)
        {
            var proxy_service = new ProxyUser();

            try
            {
                // Usamos await para esperar la respuesta de la tarea asincrónica
                var response = await proxy_service.VerifyAndCreateUser(email, code);

                if (response != null && response.Message != null)
                {
                    return Json(new { Message = response.Message });
                }
                else
                {
                    return Json(new { Message = "Código incorrecto o expirado" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = $"Error al verificar el código: {ex.Message}" });
            }
        }


        // Mostrar la vista de Login
        public ActionResult Login()
        {
            return View();
        }

        // Enviar el código de verificación para el inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password)
        {
            var proxy_service = new ProxyUser();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["ErrorMessage"] = "Correo y contraseña son obligatorios.";
                return View();
            }

            try
            {
                var response = await proxy_service.Login(email, password);

                if (!string.IsNullOrEmpty(response))
                {
                    TempData["SuccessMessage"] = response;
                    TempData["Email"] = email; 
                    return RedirectToAction("VerifyLoginCode");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View();
        }

        // Mostrar la vista para verificar el código del login
        public ActionResult VerifyLoginCode()
        {
            ViewBag.Email = TempData["Email"];
            return View();
        }

        // Verificar el código de autenticación y completar el inicio de sesión
        [HttpPost]
        public async Task<ActionResult> VerifyLoginCode(string email, string code)
        {
            var proxy_service = new ProxyUser();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
            {
                return Json(new { Message = "Email y código son obligatorios." });
            }

            try
            {
                var response = await proxy_service.VerifyCode(email, code);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
                    // Podrías almacenar el token o redirigir al usuario según sea necesario
                    return Json(new { Message = response.Message, Token = response.Token });
                }
                else
                {
                    return Json(new { Message = "Código incorrecto o expirado." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = $"Error al verificar el código: {ex.Message}" });
            }
        }



    }
}