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
    public class AmortizatioController : ApiController
    {
        // Crear una nueva amortización
        [HttpPost]
        public IHttpActionResult CreateAmortization([FromBody] Amortization newAmortization)
        {
            var amortizationLogic = new AmortizationLogic();
            try
            {
                var createdAmortization = amortizationLogic.CreateAmortization(newAmortization);
                return Ok(new { Message = "Amortización creada exitosamente", Amortization = createdAmortization });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtener todas las amortizaciones
        [HttpGet]
        public IHttpActionResult GetAllAmortizations()
        {
            var amortizationLogic = new AmortizationLogic();
            try
            {
                var amortizations = amortizationLogic.RetrieveAllAmortization();
                return Ok(new { Amortizations = amortizations });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Obtener una amortización por ID
        [HttpGet]
        public IHttpActionResult GetAmortizationById(int id)
        {
            var amortizationLogic = new AmortizationLogic();
            try
            {
                var amortization = amortizationLogic.RetrieveByIdAmortization(id);
                if (amortization == null)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Amortización no encontrada" });
                }
                return Ok(new { Amortization = amortization });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Eliminar una amortización por ID
        [HttpDelete]
        public IHttpActionResult DeleteAmortization(int id)
        {
            var amortizationLogic = new AmortizationLogic();
            try
            {
                bool deleted = amortizationLogic.Delete(id);
                if (!deleted)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Amortización no encontrada o no se pudo eliminar" });
                }

                return Ok(new { Message = "Amortización eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateAmortization([FromBody] Amortization amortization)
        {
            if (amortization == null)
            {
                return BadRequest("Los datos de la amortización no son válidos.");
            }

            var amortizationLogic = new AmortizationLogic();
            try
            {
                // Llamar a la lógica de negocio para actualizar la amortización
                bool updateSuccess = amortizationLogic.UpdateAmortization(amortization);

                if (updateSuccess)
                {
                    return Ok(new { Message = "Amortización actualizada exitosamente" });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Amortización no encontrada o no se pudo actualizar." });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Manejo de errores internos
            }
        }
    }
}