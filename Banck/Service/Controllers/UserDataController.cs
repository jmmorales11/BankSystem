using BLL;
using Entities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Http;

namespace Service.Controllers
{
    public class UserDataController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateUserData([FromBody] User_Data newUserData)
        {
            var userDataLogic = new UserDataLogic();
            var (success, message, createdUserData) = userDataLogic.Create(newUserData);

            if (success)
            {
                return Ok(new { Success = true, Message = message, UserData = createdUserData });
            }
            else
            {
                return BadRequest(JsonConvert.SerializeObject(new { Success = false, Message = message }));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllUserData()
        {
            var userDataLogic = new UserDataLogic();
            var (success, message, userDataList) = userDataLogic.RetrieveAllUserData();

            if (success)
            {
                return Ok(new { Success = true, UserDataList = userDataList });
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
            }
        }

        [HttpGet]
        public IHttpActionResult GetUserDataById(int id)
        {
            var userDataLogic = new UserDataLogic();
            var (success, message, userData) = userDataLogic.RetrieveByIdUserData(id);

            if (success)
            {
                return Ok(new { Success = true, UserData = userData });
            }
            else
            {
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteUserData(int id)
        {
            var userDataLogic = new UserDataLogic();
            var (success, message) = userDataLogic.Delete(id);

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
        public IHttpActionResult UpdateUserData([FromBody] User_Data updatedUserData)
        {
            var userDataLogic = new UserDataLogic();
            var (success, message) = userDataLogic.UpdateUser_Data(updatedUserData);

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
