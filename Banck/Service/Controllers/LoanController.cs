using BLL;
using Entities;
using Newtonsoft.Json;
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
            var (success, message, createdLoan) = loanLogic.Create(newLoan);

            if (success)
            {
                return Ok(new { Success = true, Message = message, Loan = createdLoan });
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
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
        public IHttpActionResult GetLoanById(int id)
        {
            var loanLogic = new LoanLogic();
            var (success, message, loan) = loanLogic.RetrieveByIdLoan(id);

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
    }
}
