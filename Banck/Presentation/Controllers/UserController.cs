using Entities;
using Entities.DTOs;
using Proxy;
using SL.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
                    Session["JWT_Token"] = response.Token;
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
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "No hay token disponible. Por favor, inicia sesión.";
                return RedirectToAction("Login");
            }

            // 2. Decodificar el token para obtener el rol
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

            if (roleClaim == null)
            {
                TempData["ErrorMessage"] = "El token no contiene el rol.";
                return RedirectToAction("Login");
            }

            var userRole = roleClaim.Value; // "Admin", "User", etc.

            // 3. Validar si es Admin
            if (userRole != "Admin")
            {
                // Mostrar mensaje de "No autorizado" y NO cargar la vista
                TempData["ErrorMessage"] = "No autorizado: Solo un administrador puede ver la lista de usuarios.";
                return RedirectToAction("MyProfile", "User");
                // O puedes retornar un status HTTP 403:
                // return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "No autorizado");
            }
            // Instanciar el proxy con el token
            var proxy_service = new ProxyUser(token);

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
            var token = Session["JWT_Token"] as string;
            var proxy_service = new ProxyUser(token);

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
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var token = Session["JWT_Token"] as string;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            bool isAdmin = roleClaim != null && roleClaim.Value == "Admin";
            ViewBag.IsAdmin = isAdmin; // Esta bandera se usará en la vista

            var proxyService = new ProxyUser(token);
            var userResponse = proxyService.GetUserById(id);

            if (!userResponse.Success)
            {
                TempData["ErrorMessage"] = userResponse.Message;
                return RedirectToAction("GetAllUsers");
            }

            return View(userResponse.User);
        }


        [HttpPost]
        public ActionResult EditUser(int id, User updatedUser)
        {
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyUser(token);

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
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyUser(token);
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


        //Perfil del usuario
        public ActionResult MyProfile(int? userId)
        {
            // Si se pasa userId, puedes usarlo para cargar el perfil,
            // o de lo contrario, decodificar el token.
            int id;
            if (userId.HasValue)
            {
                id = userId.Value;
            }
            else
            {
                var token = Session["JWT_Token"] as string;
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out id))
                {
                    TempData["ErrorMessage"] = "El token no contiene un id de usuario válido.";
                    return RedirectToAction("Login");
                }
            }

            var proxyService = new ProxyUser(Session["JWT_Token"] as string);
            var userResponse = proxyService.GetUserById(id);

            if (!userResponse.Success)
            {
                TempData["ErrorMessage"] = userResponse.Message;
                return RedirectToAction("GetAllUsers"); // O la acción que consideres apropiada
            }

            return View(userResponse.User);
        }



        // ===================== ACCIONES PARA RECUPERAR CONTRASEÑA =====================

        // GET: Mostrar el formulario para solicitar el código de recuperación
        public ActionResult RecoverPassword()
        {
            return View();
        }

        // POST: Enviar el código de recuperación al correo del usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverPassword(string email)
        {
            var proxy_service = new ProxyUser();

            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["ErrorMessage"] = "El email es requerido.";
                return View();
            }

            try
            {
                var response = await proxy_service.RecoverPassword(email);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    // Almacenar el email para el siguiente paso
                    TempData["Email"] = email;
                    return RedirectToAction("ResetPassword");
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

        // GET: Mostrar el formulario para ingresar el código recibido y la nueva contraseña
        public ActionResult ResetPassword()
        {
            ViewBag.Email = TempData["Email"];
            return View();
        }

        // POST: Validar el código y actualizar la contraseña
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(string email, string code, string newPassword)
        {
            var proxy_service = new ProxyUser();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(newPassword))
            {
                TempData["ErrorMessage"] = "Todos los campos son obligatorios.";
                return View();
            }

            try
            {
                var request = new ResetPasswordRequest
                {
                    Email = email,
                    Code = code,
                    NewPassword = newPassword
                };

                var response = await proxy_service.ResetPassword(request);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction("Login");
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


        [HttpGet]
        public ActionResult Details(int id)
        {
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "No hay token disponible. Por favor, inicie sesión.";
                return RedirectToAction("Login");
            }

            // Decodificar el token para obtener el rol y el UserId (del usuario autenticado)
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            bool isAdmin = roleClaim != null && roleClaim.Value == "Admin";
            ViewBag.IsAdmin = isAdmin;

            // Obtener el id del usuario autenticado para redirección (suponiendo que se almacena en el claim "UserId")
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null)
            {
                ViewBag.UserId = int.Parse(userIdClaim.Value);
            }

            var proxyService = new ProxyUser(token);
            var userResponse = proxyService.GetUserById(id);

            if (!userResponse.Success)
            {
                TempData["ErrorMessage"] = userResponse.Message;
                return RedirectToAction("GetAllUsers");
            }

            return View(userResponse.User);
        }





    }
}