using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;
using Entities;
using SLC;

namespace BLL
{
    public class UserLogic
    {
        public List<User> RetrieveAllUser()
        {
            List<User> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<User>();
            }
            return res;
        }
        public User RetrieveByIdUser(int id)
        {
            User res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<User>(p => p.user_id == id);
            }
            return res;
        }
        public bool Delete(int id)
        {
            bool res = false;
            var user = RetrieveByIdUser(id);
            if (user != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(user);
                }
            }
            return res;
        }
        public bool UpdateUser(User user)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Llamamos al repositorio para realizar la actualización del usuario
                result = r.Update(user);
            }
            return result;
        }


        public User Create(User users)
        {
            User res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                users.status = 1;
                users.registration_date = DateTime.Now;

                    res = r.Create(users);

                return res;
            }

        }

        public bool validateExistingUser(User user)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                User existingUser = r.Retrieve<User>(u => u.email == user.email);
                if(existingUser != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public User Authenticate(string email, string password)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                User user = r.Retrieve<User>(u => u.email == email && u.password == password);

                return user; // Retorna `null` si no encuentra usuario
            }
        }




    }
}
