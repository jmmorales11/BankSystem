using Entities;
using Proxy;
using System;
using System.Collections.Generic;
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
            var proxyService = new ProxyUser();
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
            var proxyService = new ProxyLoan();
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
            var proxyService = new ProxyLoan();
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
