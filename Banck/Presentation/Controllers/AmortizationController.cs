using Entities;
using Entities.DTOs;
using Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class AmortizationController : Controller
    {
        [HttpGet]
        // Acción para mostrar el cronograma de amortización de un préstamo
        public async Task<ActionResult> Amortization(int loanId)
        {
            var proxy = new ProxyAmortization();
            try
            {
                AmortizationResponseDto response = await proxy.GetLoanAmortizationSchedule(loanId);
                if (response != null && response.Success)
                {
                    return View(response.AmortizationSchedule);
                }
                else
                {
                    TempData["ErrorMessage"] = response?.Message ?? "No se encontró cronograma de amortización para el préstamo indicado.";
                    // Redirige a la lista de préstamos. Puedes ajustar la redirección según tu lógica.
                    return RedirectToAction("ListLoansByUser", "Loan");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ListLoansByUser", "Loan");
            }
        }



    }
}
