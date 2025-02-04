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

        public User Authenticate(string email, string password)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                // Recuperar el usuario por correo electrónico y contraseña
                User user = r.Retrieve<User>(u => u.email == email && u.password == password);

                if (user == null)
                {
                    throw new System.UnauthorizedAccessException("Credenciales incorrectas.");
                }

                return user; // Usuario autenticado correctamente
            }
        }



    }
}
