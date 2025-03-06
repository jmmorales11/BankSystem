using BLL;
using Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using log4net;

namespace Service.Controllers
{
    public class LoanController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoanController));

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public IHttpActionResult CreateLoan([FromBody] Loan newLoan)
        {
            var loanLogic = new LoanLogic();

            var (retrieveSuccess, retrieveMessage, userLoans) = loanLogic.RetrieveLoansByUserId(newLoan.user_id);
            if (userLoans != null && userLoans.Any(l => l.status == 1))
            {
                log.Warn($"El usuario {newLoan.user_id} ya tiene un préstamo activo.");
                return Content(HttpStatusCode.BadRequest, new { Success = false, Message = "El usuario ya tiene un préstamo activo." });
            }

            newLoan.application_date = DateTime.Now;
            newLoan.status = 1;
            var (success, message, createdLoan) = loanLogic.Create(newLoan);

            if (!success)
            {
                log.Error($"Error al crear el préstamo para el usuario {newLoan.user_id}: {message}");
                return Content(HttpStatusCode.BadRequest, new { Success = false, Message = message });
            }

            var amortizationLogic = new AmortizationLogic();
            var (scheduleSuccess, scheduleMessage, schedule) = amortizationLogic.GenerateAndSaveSchedule(createdLoan);

            if (scheduleSuccess)
            {
                createdLoan.Amortizations = schedule;
            }
            else
            {
                scheduleMessage = "Préstamo creado, pero hubo un problema al generar la amortización: " + scheduleMessage;
                log.Warn(scheduleMessage);
            }
            log.Info($"Préstamo creado exitosamente para el usuario {newLoan.user_id}");
            return Ok(new { Success = true, Message = message + " " + scheduleMessage, Loan = createdLoan });
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetAllLoans()
        {
            var loanLogic = new LoanLogic();
            var (success, message, loans) = loanLogic.RetrieveAllLoan();

            if (success)
            {
                log.Info("Préstamos recuperados exitosamente.");
                return Ok(new { Success = true, Loans = loans });
            }
            else
            {
                log.Error("Error al recuperar los préstamos: " + message);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IHttpActionResult GetLoanById(int userId)
        {
            var loanLogic = new LoanLogic();
            var (success, message, loan) = loanLogic.RetrieveByIdLoan(userId);

            if (success)
            {
                log.Info($"Préstamo recuperado exitosamente para el usuario: {userId}");
                return Ok(new { Success = true, Loan = loan });
            }
            else
            {
                log.Warn($"No se encontró el préstamo para el usuario con el ID: {userId}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult DeleteLoan(int id)
        {
            var loanLogic = new LoanLogic();
            var (success, message) = loanLogic.Delete(id);

            if (success)
            {
                log.Info($"Préstamo eliminado exitosamente: {id}");
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                log.Warn($"No se pudo eliminar el préstamo con el ID: {id}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        public IHttpActionResult UpdateLoan([FromBody] Loan loan)
        {
            var loanLogic = new LoanLogic();
            var (success, message) = loanLogic.UpdateLoan(loan);

            if (success)
            {
                log.Info($"Préstamo actualizado exitosamente: {loan.loan_id}");
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                log.Warn($"No se pudo actualizar el préstamo con el ID: {loan.loan_id}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("api/Loan/GetLoansByUserId/{userId}")]
        public IHttpActionResult GetLoansByUserId(int userId)
        {
            var loanLogic = new LoanLogic();
            try
            {
                // Filtramos los préstamos por user_id
                var (success, message, loans) = loanLogic.RetrieveLoansByUserId(userId);

                if (success)
                {
                    log.Info($"Préstamos recuperados exitosamente para el usuario: {userId}");
                    return Ok(new { Success = true, Loans = loans });
                }
                else
                {
                    log.Warn($"No se encontraron préstamos para el usuario con el ID: {userId}");
                    return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error al recuperar los préstamos para el usuario con el ID: {userId}", ex);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("api/Loan/GetLoanDetails/{loanId}")]

        public IHttpActionResult GetLoanDetails(int loanId)
        {
            var loanLogic = new LoanLogic();
            var (success, message, data) = loanLogic.RetrieveLoanDetails(loanId);

            if (!success)
            {
                return Content(System.Net.HttpStatusCode.NotFound, new { Success = false, Message = message });
            }

            return Ok(new
            {
                Success = true,
                Message = message,
                Data = data
            });
        }

    }
}
