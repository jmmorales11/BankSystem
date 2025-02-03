using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using BLL;
using SLC;

namespace Service.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public User CreateUser(User newUser)
        {
            var BL = new UserLogic();
            var NewUser = BL.Create(newUser);
            return NewUser;
        }
    }
}
