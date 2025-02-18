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
        public (bool Success, string Message, List<User> UserList) RetrieveAllUser()
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var userList = r.RetrieveAll<User>();
                    return (true, "Usuarios recuperados exitosamente", userList);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar usuarios: {ex.Message}", null);
            }
        }
        public (bool Success, string Message, User User) RetrieveByIdUser(int id)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var user = r.Retrieve<User>(p => p.user_id == id);
                    if (user == null)
                    {
                        return (false, "Usuario no encontrado", null);
                    }
                    return (true, "Usuario encontrado", user);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar usuario: {ex.Message}", null);
            }
        }

        public (bool Success, string Message) Delete(int id)
        {
            try
            {
                var user = RetrieveByIdUser(id).User;
                if (user == null)
                {
                    return (false, "Usuario no encontrado");
                }

                using (var r = RepositoryFactory.CreateRepository())
                {
                    bool deleted = r.Delete(user);
                    return deleted
                        ? (true, "Usuario eliminado exitosamente")
                        : (false, "No se pudo eliminar el usuario");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar usuario: {ex.Message}");
            }
        }

        public (bool Success, string Message) UpdateUser(User user)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    bool updated = r.Update(user);
                    return updated
                        ? (true, "Usuario actualizado exitosamente")
                        : (false, "No se pudo actualizar el usuario");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar usuario: {ex.Message}");
            }
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
