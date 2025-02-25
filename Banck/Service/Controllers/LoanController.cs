using BLL;
using Entities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Http;

namespace Service.Controllers
{
    public class LoanController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateLoan([FromBody] Loan newLoan)
        {
            var loanLogic = new LoanLogic();
            newLoan.application_date = DateTime.Now;
            newLoan.status = 1;
            var (success, message, createdLoan) = loanLogic.Create(newLoan);

            if (!success)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(new { Success = false, Message = message }));
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
            }

            return Ok(new { Success = true, Message = message + " " + scheduleMessage, Loan = createdLoan });
        }


        [HttpGet]
        public IHttpActionResult GetAllLoans()
        {
            var loanLogic = new LoanLogic();
            var (success, message, loans) = loanLogic.RetrieveAllLoan();

            if (success)
            {
                return Ok(new { Success = true, Loans = loans });
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
            }
        }

        [HttpGet]
        public IHttpActionResult GetLoanById(int userId)
        {
            var loanLogic = new LoanLogic();
            var (success, message, loan) = loanLogic.RetrieveByIdLoan(userId);

            if (success)
            {
                return Ok(new { Success = true, Loan = loan });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteLoan(int id)
        {
            var loanLogic = new LoanLogic();
            var (success, message) = loanLogic.Delete(id);

            if (success)
            {
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateLoan([FromBody] Loan loan)
        {
            var loanLogic = new LoanLogic();
            var (success, message) = loanLogic.UpdateLoan(loan);

            if (success)
            {
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }


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
                    return Ok(new { Success = true, Loans = loans });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = ex.Message });
            }
        }

    }
}
