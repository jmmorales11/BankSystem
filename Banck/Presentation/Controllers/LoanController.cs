using Entities;
using Proxy;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Presentation.Controllers
{
    public class LoanController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoanController));

        // Acción para mostrar la lista de usuarios
        [HttpGet]
        public async Task<ActionResult> ListUser()
        {
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                log.Error("No hay token disponible.");
                return RedirectToAction("Login", "User");
            }
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "No hay token disponible. Por favor, inicia sesión.";
                log.Error("No hay token disponible.");
                return RedirectToAction("Login");
            }

            // 2. Decodificar el token para obtener el rol
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

            if (roleClaim == null)
            {
                TempData["ErrorMessage"] = "El token no contiene el rol.";
                log.Error("El token no contiene el rol.");
                return RedirectToAction("Login", "User");
            }

            var userRole = roleClaim.Value; // "Admin", "User", etc.

            // 3. Validar si es Admin
            if (userRole != "Admin")
            {
                // Mostrar mensaje de "No autorizado" y NO cargar la vista
                TempData["ErrorMessage"] = "No autorizado: Solo un administrador puede ver la lista de usuarios.";
                log.Info("No autorizado: Solo un administrador puede ver la lista de usuarios.");
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
                    log.Info("Usuarios recuperados exitosamente.");
                    return View(response.Users); // Pasamos los usuarios a la vista
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    log.Warn($"Error al recuperar los usuarios: {response.Message}");
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                log.Error("Error al recuperar los usuarios", ex);
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> ListLoansByUser(int userId)
        {
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                log.Error("No hay token disponible.");
                return RedirectToAction("Login", "User");
            }
            var proxyService = new ProxyLoan(token);
            // Guardamos el userId en la sesión y en el ViewBag
            Session["UserId"] = userId;
            ViewBag.UserId = userId;

            // Decodificamos el token para obtener el rol
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            bool isAdmin = roleClaim != null && roleClaim.Value == "Admin";
            ViewBag.IsAdmin = isAdmin;

            try
            {
                // Obtener los préstamos del usuario por ID
                var loansResponse = await proxyService.GetLoansByUserId(userId);
                if (loansResponse.Success)
                {
                    log.Info($"Préstamos recuperados exitosamente para el usuario: {userId}");
                    return View(loansResponse.Loans);
                }
                else
                {
                    TempData["ErrorMessage"] = loansResponse.Message;
                    log.Warn($"Error al recuperar los préstamos para el usuario: {userId} - {loansResponse.Message}");
                    return View(new List<Loan>());
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                log.Error($"Error al recuperar los préstamos para el usuario: {userId}", ex);
                return View(new List<Loan>());
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

            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                log.Error("No hay token disponible.");
                return RedirectToAction("Login", "User");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            ViewBag.IsAdmin = (roleClaim != null && roleClaim.Value == "Admin");
            ViewBag.UserId = userId;
            log.Info($"Accediendo a la página de creación de préstamo para el usuario: {userId}");
            return View(loan);
        }


        // Acción para crear el préstamo
        [HttpPost]
        public async Task<ActionResult> CreateLoan(Loan loan)
        {
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                log.Error("No hay token disponible.");
                return RedirectToAction("Login", "User");
            }
            var proxyService = new ProxyLoan(token);
            try
            {
                var response = await proxyService.CreateLoan(loan);
                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Préstamo creado exitosamente.";
                    log.Info($"Préstamo creado exitosamente para el usuario: {loan.user_id}");
                    return RedirectToAction("ListLoansByUser", new { userId = loan.user_id });
                }
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    log.Warn($"Error al crear el préstamo para el usuario: {loan.user_id} - {response.Message}");
                    return View(loan);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                log.Error($"Error al crear el préstamo para el usuario: {loan.user_id}", ex);
                return View(loan);
            }
        }

        [HttpGet]
        public async Task<ActionResult> LoanDetails(int loanId)
        {
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "No hay token disponible. Por favor, inicia sesión.";
                return RedirectToAction("Login");
            }
            var proxyLoan = new ProxyLoan(token);

            try
            {
                var response = await proxyLoan.GetLoanDetails(loanId);
                if (response != null && response.Success && response.Data != null)
                {
                    // Si EndDate es null, la última cuota no ha sido pagada
                    if (!response.Data.EndDate.HasValue)
                    {
                        TempData["ErrorMessage"] = "El préstamo aún no ha sido pagado en su totalidad. No se puede ver el detalle.";
                        return RedirectToAction("ListLoansByUser", new { userId = Session["UserId"] });
                    }
                    // Se pasó el DTO a la vista solo si EndDate tiene valor
                    return View(response.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = response?.Message ?? "Error al recuperar el detalle del préstamo.";
                    return RedirectToAction("ListLoansByUser", new { userId = Session["UserId"] });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ListLoansByUser", new { userId = Session["UserId"] });
            }
        }


    }
}
