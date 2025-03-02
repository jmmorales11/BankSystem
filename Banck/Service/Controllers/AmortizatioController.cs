using BLL;
using Entities;
using System;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using log4net;

namespace Service.Controllers
{
    public class AmortizationController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AmortizationController));

        //Obtener Amortización de un préstamo
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("api/loan/{loanId}/amortization")]
        public IHttpActionResult GetLoanAmortizationSchedule(int loanId)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, amortizations) = amortizationLogic.RetrieveAllAmortization();

            if (!success)
            {
                log.Error($"Error al recuperar las amortizaciones: {message}");
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
            }

            var schedule = amortizations.FindAll(a => a.loan_id == loanId);
            if (schedule.Count == 0)
            {
                log.Warn($"No se encontró cronograma de amortización para el préstamo con ID: {loanId}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = "No se encontró cronograma de amortización para el préstamo indicado." });
            }

            log.Info($"Cronograma de amortización recuperado exitosamente para el préstamo con ID: {loanId}");
            return Ok(new { Success = true, AmortizationSchedule = schedule });
        }
        [Authorize(Roles = "Admin,User")]
        // Nuevo endpoint para registrar el pago de una cuota
        [HttpPost]
        [Route("api/amortization/pay/{id}")]
        public IHttpActionResult PayInstallment(int id)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, amortization) = amortizationLogic.PayInstallment(id);

            if (success)
            {
                log.Info($"Cuota pagada exitosamente para la amortización con ID: {id}");
                return Ok(new { Success = true, Message = message, Amortization = amortization });
            }
            else
            {
                log.Warn($"Error al pagar la cuota para la amortización con ID: {id} - {message}");
                return Content(HttpStatusCode.BadRequest, new { Success = false, Message = message });
            }
        }


    }
}
