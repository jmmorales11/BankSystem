using Entities;
using Proxy;
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
            _emailService = new EmailService(); 
        }

        public ActionResult Index()
        {
            return View();
        }


        //ACCIÓN PARA ENVIAR EL CÓDIGO DE VERFICACIÓN ANTES DE CREAR EL USUARIO
        public ActionResult CreateUser()
        {
            return View();
        }

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

                    if (createdUserResponse.Success)
                    {
                        TempData["SuccessMessage"] = createdUserResponse.Message;
                        return RedirectToAction("VerifyAndCreateUser");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = createdUserResponse.Message;
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }

            return View(newUser);
        }



        //ACCIÓN PARA VERIFICAR EL CÓDIGO QUE SE ENVIÓ Y CREAR EL USUARIO
        public ActionResult VerifyAndCreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> VerifyAndCreateUser(string email, string code)
        {
            var proxy_service = new ProxyUser();

            try
            {
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


        // ACCIÓN PARA ENVIAR CÓDIGO DE VERRIFICACIÓN ANTES DE INGRESAR AL SISTEMA 
        public ActionResult Login()
        {
            return View();
        }

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

                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    TempData["Email"] = response.Email;
                    return RedirectToAction("VerifyLoginCode");
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }


        //ACCIÓN PARA VERIFICAR EL CÓDIGO DE ACCESO DE USUARIO
        public ActionResult VerifyLoginCode()
        {
            ViewBag.Email = TempData["Email"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> VerifyLoginCode(string email, string code)
        {
            var proxy_service = new ProxyUser();

            if (string.IsNullOrWhiteSpace(code))
            {
                return Json(new { Message = "Ingrese el código" });
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