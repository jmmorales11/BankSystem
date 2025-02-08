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
            var proxy_service = new proxy();

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
            var proxy_service = new proxy();

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

    }
}