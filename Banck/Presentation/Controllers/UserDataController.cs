using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entities;
using Proxy;

namespace Presentation.Controllers
{
    public class UserDataController : Controller
    {
        // Acción para mostrar la lista de usuarios
        [HttpGet]
        public async Task<ActionResult> ListUserData()
        {
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
            var proxyService = new ProxyUser(token);
            try
            {
                var response = await proxyService.GetAllUsers();
                if (response.Success)
                {
                    return View(response.Users); // Pasamos los usuarios a la vista
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


        // Acción para mostrar el formulario de creación o actualización de datos de usuario
        [HttpGet]
        public async Task<ActionResult> CreateUserData(int userId)
        {
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyUserData(token); // Usar el proxy adecuado para UserData

            // Decodificar el token para obtener el rol
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            bool isAdmin = roleClaim != null && roleClaim.Value == "Admin";
            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserId = userId;

            try
            {
                // Obtenemos los datos de User_Data para el usuario con el userId
                var response = await proxyService.GetUserDataById(userId);

                // Agregamos un console.log (Debug.WriteLine) para ver la respuesta
                Debug.WriteLine($"Respuesta obtenida para el usuario {userId}: {response.Success}, Datos: {response.UserData}");

                if (response != null && response.Success)
                {
                    // Si los datos existen, los pasamos al formulario para editar
                    return View(response.UserData);
                }
                else
                {
                    // Si no existen datos, pasamos un modelo vacío para crear
                    return View(new User_Data { user_id = userId });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ListUserData");
            }
        }




        // Acción POST para crear o actualizar los datos de usuario
        [HttpPost]
        public async Task<ActionResult> CreateUserData(User_Data userData)
        {
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyUserData(token);
            try
            {
                // Si el ID de User_Data es 0, es creación, si no, es actualización
                var response = userData.user_data_id == 0
                    ? await proxyService.CreateUserData(userData) // Crear si no existe
                    : await proxyService.UpdateUserData(userData.user_data_id, userData); // Actualizar si ya existe

                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Datos de usuario guardados correctamente.";
                    return RedirectToAction("ListUserData"); // Redirigir a la vista con los datos actualizados
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    return View(userData); // Volver a mostrar el formulario con los datos
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(userData); // Volver a mostrar el formulario con el error
            }
        }

        // Acción para ver los datos de usuario en modo solo lectura
        [HttpGet]
        public async Task<ActionResult> ViewUserData(int userId)
        {
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyUserData(token);

            // Decodificar el token para obtener el rol
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            bool isAdmin = roleClaim != null && roleClaim.Value == "Admin";
            ViewBag.IsAdmin = isAdmin;
            ViewBag.UserId = userId;

            try
            {
                var response = await proxyService.GetUserDataById(userId);

                if (response != null && response.Success)
                {
                    // Mostrar los datos en modo lectura
                    return View("ViewUserData", response.UserData);
                }
                else
                {
                    TempData["ErrorMessage"] = response?.Message ?? "No se encontraron datos para el usuario.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al obtener los datos del usuario: {ex.Message}";
                return View();
            }
        }



    }
}
