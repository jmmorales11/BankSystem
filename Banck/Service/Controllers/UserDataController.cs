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
    public class UserDataController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateUserData([FromBody] User_Data newUserData)
        {
            var userDataLogic = new UserDataLogic();
            try
            {
                var createdUserData = userDataLogic.Create(newUserData);
                return Ok(new { Message = "Datos de usuario creados exitosamente", UserData = createdUserData });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Obtener todos los registros de User_Data
        [HttpGet]
        public IHttpActionResult GetAllUserData()
        {
            var userDataLogic = new UserDataLogic();
            try
            {
                var userDataList = userDataLogic.RetrieveAllUserData();
                return Ok(new { UserDataList = userDataList });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: Obtener un User_Data por ID
        [HttpGet]
        public IHttpActionResult GetUserDataById(int id)
        {
            var userDataLogic = new UserDataLogic();
            try
            {
                var userData = userDataLogic.RetrieveByIdUserData(id);
                if (userData == null)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Datos de usuario no encontrados" });
                }
                return Ok(new { UserData = userData });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: Eliminar un User_Data por ID
        [HttpDelete]
        public IHttpActionResult DeleteUserData(int id)
        {
            var userDataLogic = new UserDataLogic();
            try
            {
                bool deleted = userDataLogic.Delete(id);
                if (!deleted)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Datos de usuario no encontrados o no se pudieron eliminar" });
                }
                return Ok(new { Message = "Datos de usuario eliminados exitosamente" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateUserData([FromBody] User_Data updatedUserData)
        {
            try
            {
                var BL = new UserDataLogic();
                bool isUpdated = BL.UpdateUser_Data(updatedUserData);

                if (!isUpdated)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "No se encontró el dato del usuario para actualizar." });
                }

                return Ok(new { Message = "Datos del usuario actualizados exitosamente." });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor: " + ex.Message });
            }
        }

    }
}