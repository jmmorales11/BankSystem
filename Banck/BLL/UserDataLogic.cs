using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserDataLogic
    {
        public User_Data Create(User_Data users)
        {
            User_Data res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {

                res = r.Create(users);

                return res;
            }

        }

        public List<User_Data> RetrieveAllUserData()
        {
            List<User_Data> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<User_Data>();
            }
            return res;
        }

        public User_Data RetrieveByIdUserData(int id)
        {
            User_Data res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<User_Data>(p => p.user_data_id == id);
            }
            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;
            var user = RetrieveByIdUserData(id);
            if (user != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(user);
                }
            }
            return res;
        }
        public bool UpdateUser_Data(User_Data user_Data)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Actualiza el préstamo y devuelve un booleano indicando si fue exitoso
                result = r.Update(user_Data);
            }
            return result;
        }

    }
}
