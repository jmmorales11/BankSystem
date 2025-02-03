using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;
using SLC;

namespace BLL
{
    public class UserLogic
    {
        public User Create(User users)
        {
            User res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                    res = r.Create(users);

                return res;
            }

        }
    }
}
