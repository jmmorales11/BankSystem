using Entities;
using Proxy;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class LoanController : Controller
    {
        // Acción para mostrar la lista de usuarios
        [HttpGet]
        public async Task<ActionResult> ListUser()
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

        // Acción para listar todos los préstamos de un usuario
        [HttpGet]
        public async Task<ActionResult> ListLoansByUser(int userId)
        {
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyLoan(token);
            try
            {
                // Obtener los préstamos del usuario por ID
                var loansResponse = await proxyService.GetLoansByUserId(userId);

                if (loansResponse.Success)
                {
                    return View(loansResponse.Loans); // Pasamos los préstamos al modelo de la vista
                }
                else
                {
                    TempData["ErrorMessage"] = loansResponse.Message;
                    return View(new List<Loan>()); // Devolvemos una lista vacía si no se encontraron préstamos
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(new List<Loan>()); // Devolvemos una lista vacía en caso de error
            }
        }






        // Acción para mostrar el formulario de creación de un préstamo
        [HttpGet]
        public ActionResult CreateLoan(int userId)
        {
            // Crear un modelo vacío para el préstamo
            var loan = new Loan
            {
                user_id = userId
            };
            return View(loan);
        }

        // Acción para crear el préstamo
        [HttpPost]
        public async Task<ActionResult> CreateLoan(Loan loan)
        {
            var token = Session["JWT_Token"] as string;
            var proxyService = new ProxyLoan(token);
            try
            {
                var response = await proxyService.CreateLoan(loan);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Préstamo creado exitosamente.";
                    return RedirectToAction("ListLoansByUser", new { userId = loan.user_id });
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    return View(loan);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(loan);
            }
        }




    }
}
