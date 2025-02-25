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
                        TempData["Email"] = createdUserResponse.User.email;
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
            ViewBag.Email = TempData["Email"];
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
                    TempData["Email"] = email; // Guardar el correo para usar en HomeController
                                               // Almacenar el token en el LocalStorage usando JavaScript
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


        //OBTENER TODOS LOS USUARIOS
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var proxy_service = new ProxyUser();
            try
            {
                var response = await proxy_service.GetAllUsers();
                if (response.Success)
                {
                    return View(response.Users);  
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // Acción para eliminar un usuario por ID
        public ActionResult DeleteUser(int id)
        {
            var proxy_service = new ProxyUser();

            try
            {
                var response = proxy_service.DeleteUser(id);

                if (response != null && response.Success)
                {
                    TempData["SuccessMessage"] = "Usuario eliminado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = response?.Message ?? "No se pudo eliminar al usuario. Respuesta vacía.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el usuario: {ex.Message}";
            }

            return RedirectToAction("GetAllUsers");
        }



        //ACTUALIZAR UN USUARIOS
        public ActionResult EditUser(int id)
        {
            var proxyService = new ProxyUser();
            var userResponse = proxyService.GetUserById(id); // Llamar al proxy para obtener el usuario

            if (!userResponse.Success)
            {
                TempData["ErrorMessage"] = userResponse.Message;
                return RedirectToAction("GetAllUsers"); // Redirigir a la lista si no se encuentra el usuario
            }

            return View(userResponse.User); // Pasa el usuario a la vista de edición
        }

        [HttpPost]
        public ActionResult EditUser(int id, User updatedUser)
        {
            var proxyService = new ProxyUser();

            try
            {
                var response = proxyService.UpdateUser(id, updatedUser);  // Llamada al servicio para actualizar

                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;  // Mensaje de éxito
                    return RedirectToAction("GetAllUsers");  // Redirigir a la lista de usuarios
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;  // Mensaje de error
                    return View(updatedUser);  // Mostrar de nuevo el formulario de edición con errores
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(updatedUser);  // Mostrar el formulario de edición con errores
            }
        }


        // GET: Mostrar el formulario para crear un usuario de forma directa
        public ActionResult DirectCreateUser()
        {
            return View();
        }

        // POST: Crear usuario de forma directa (sin verificación)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DirectCreateUser(User newUser)
        {
            var proxyService = new ProxyUser();
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await proxyService.DirectCreateUser(newUser);
                    if (response != null && response.Success)
                    {
                        TempData["SuccessMessage"] = response.Message;
                        return RedirectToAction("GetAllUsers");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = response?.Message ?? "No se pudo crear el usuario.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return View(newUser);
        }

    }
}