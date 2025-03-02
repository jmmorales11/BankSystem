using Entities;
using Entities.DTOs;
using Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Presentation.Controllers
{
    public class AmortizationController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AmortizationController));

        [HttpGet]
        // Acción para mostrar el cronograma de amortización de un préstamo
        public async Task<ActionResult> Amortization(int loanId)
        {
            var token = Session["JWT_Token"] as string;
            var proxy = new ProxyAmortization(token);

            int userId = Convert.ToInt32(Session["UserId"]);
            ViewBag.UserId = userId;
            try
            {
                AmortizationResponseDto response = await proxy.GetLoanAmortizationSchedule(loanId);
                if (response != null && response.Success)
                {
                    log.Info($"Cronograma de amortización recuperado exitosamente para el préstamo con ID: {loanId}");
                    return View(response.AmortizationSchedule);
                }
                else
                {
                    TempData["ErrorMessage"] = response?.Message ?? "No se encontró cronograma de amortización para el préstamo indicado.";
                    log.Warn($"No se encontró cronograma de amortización para el préstamo con ID: {loanId}");
                    // Redirige a la lista de préstamos. Puedes ajustar la redirección según tu lógica.
                    return RedirectToAction("ListLoansByUser", "Loan");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                log.Error($"Error al recuperar el cronograma de amortización para el préstamo con ID: {loanId}", ex);
                return RedirectToAction("ListLoansByUser", "Loan");
            }
        }

        [HttpPost]
        // Acción para registrar el pago de una cuota usando el proxy
        public async Task<JsonResult> PayInstallment(int id)
        {
            var token = Session["JWT_Token"] as string;
            var proxy = new ProxyAmortization(token);
            try
            {
                // Invocamos el método del proxy que llama al endpoint de pago en el servicio
                var response = await proxy.PayInstallment(id);
                if (response.Success)
                {
                    log.Info($"Cuota pagada exitosamente para la amortización con ID: {id}");
                }
                else
                {
                    log.Warn($"Error al pagar la cuota para la amortización con ID: {id} - {response.Message}");
                }
                return Json(new { success = response.Success, message = response.Message });
            }
            catch (Exception ex)
            {
                log.Error($"Error al pagar la cuota para la amortización con ID: {id}", ex);
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}
