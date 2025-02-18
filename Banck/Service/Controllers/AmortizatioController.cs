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
        // Crear una nueva amortización
        [HttpPost]
        public IHttpActionResult CreateAmortization([FromBody] Amortization newAmortization)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, createdAmortization) = amortizationLogic.CreateAmortization(newAmortization);

            if (success)
            {
                return Ok(new { Success = true, Message = message, Amortization = createdAmortization });
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
        }

        // Obtener todas las amortizaciones
        [HttpGet]
        public IHttpActionResult GetAllAmortizations()
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, amortizations) = amortizationLogic.RetrieveAllAmortization();

            if (success)
            {
                return Ok(new { Success = true, Amortizations = amortizations });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
        }

        // Obtener una amortización por ID
        [HttpGet]
        public IHttpActionResult GetAmortizationById(int id)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message, amortization) = amortizationLogic.RetrieveByIdAmortization(id);

            if (success)
            {
                return Ok(new { Success = true, Amortization = amortization });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
        }

        // Eliminar una amortización por ID
        [HttpDelete]
        public IHttpActionResult DeleteAmortization(int id)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message) = amortizationLogic.Delete(id);

            if (success)
            {
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
        }

        // Actualizar una amortización
        [HttpPut]
        public IHttpActionResult UpdateAmortization([FromBody] Amortization amortization)
        {
            var amortizationLogic = new AmortizationLogic();
            var (success, message) = amortizationLogic.UpdateAmortization(amortization);

            if (success)
            {
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
        }
    }
}
