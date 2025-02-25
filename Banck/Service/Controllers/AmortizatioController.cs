using BLL;
using Entities;
using System;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;

namespace Service.Controllers
{
    public class AmortizationController : ApiController
    {
        //Obtener Amortización de un préstamo

        [HttpGet]
        [Route("api/loan/{loanId}/amortization")]
        public IHttpActionResult GetLoanAmortizationSchedule(int loanId)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, amortizations) = amortizationLogic.RetrieveAllAmortization();

            if (!success)
            {
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
            }

            var schedule = amortizations.FindAll(a => a.loan_id == loanId);
            if (schedule.Count == 0)
            {
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = "No se encontró cronograma de amortización para el préstamo indicado." });
            }

            return Ok(new { Success = true, AmortizationSchedule = schedule });
        }

        // Nuevo endpoint para registrar el pago de una cuota
        [HttpPost]
        [Route("api/amortization/pay/{id}")]
        public IHttpActionResult PayInstallment(int id)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, amortization) = amortizationLogic.PayInstallment(id);

            if (success)
            {
                return Ok(new { Success = true, Message = message, Amortization = amortization });
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, new { Success = false, Message = message });
            }
        }


    }
}
