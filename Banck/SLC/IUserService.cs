using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTOs;
namespace SLC
{
    public interface IUserService
    {
        UserCreationResponse Create(User users);
        Task<UserCreationResponse> VerifyAndCreateUser(string email, string code);
    }
}
