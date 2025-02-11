using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;


namespace Service.Controllers
{
    public class LoanController : ApiController
    {
        // GET: Loan
        [HttpPost]
        public IHttpActionResult CreateLoan([FromBody] Loan newLoan)
        {
            var loanLogic = new LoanLogic();
            try
            {
                // Se debe agregar alguna validación de negocio aquí, si corresponde
                var createdLoan = loanLogic.Create(newLoan);

                return Ok(new { Message = "Préstamo creado exitosamente", Loan = createdLoan });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllLoans()
        {
            var loanLogic = new LoanLogic();
            try
            {
                var loans = loanLogic.RetrieveAllLoan();
                return Ok(new { Loans = loans });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Endpoint para recuperar un préstamo por ID
        [HttpGet]
        public IHttpActionResult GetLoanById(int id)
        {
            var loanLogic = new LoanLogic();
            try
            {
                var loan = loanLogic.RetrieveByIdLoan(id);
                if (loan == null)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Préstamo no encontrado" });
                }
                return Ok(new { Loan = loan });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Endpoint para eliminar un préstamo por ID
        [HttpDelete]
        public IHttpActionResult DeleteLoan(int id)
        {
            var loanLogic = new LoanLogic();
            try
            {
                bool deleted = loanLogic.Delete(id);
                if (!deleted)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Préstamo no encontrado o no se pudo eliminar" });
                }

                return Ok(new { Message = "Préstamo eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}