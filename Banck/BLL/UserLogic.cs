using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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
                users.role = "Editor";

                res = r.Create(users);

                return res;
            }

        }

        public User CreateUser(User users)
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
                // Encriptar la contraseña ingresada para compararla con la almacenada
                string encryptedPassword = EncryptPassword(password);
                return r.Retrieve<User>(u => u.email.ToLower() == email.ToLower() && u.password == encryptedPassword);
            }
        }


        public (bool Success, string Message) ResetPassword(string email, string newPassword)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    // Buscar el usuario por correo
                    var user = r.Retrieve<User>(u => u.email == email);
                    if (user == null)
                    {
                        return (false, "Usuario no encontrado");
                    }
                    // Actualizar la contraseña
                    user.password = newPassword; 
                    bool updated = r.Update(user);
                    return updated
                        ? (true, "Contraseña actualizada exitosamente")
                        : (false, "No se pudo actualizar la contraseña");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar la contraseña: {ex.Message}");
            }
        }

        public User RetrieveByEmail(string email)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                return r.Retrieve<User>(u => u.email == email);
            }
        }


        // Método para validar y encriptar la contraseña
        public (bool IsSafe, string EncryptedPassword, string Message) ValidateAndEncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return (false, null, "La contraseña no puede estar vacía.");

            // Validación: al menos 8 caracteres
            if (password.Length < 8)
                return (false, null, "La contraseña debe tener al menos 8 caracteres.");

            // Debe contener al menos una letra mayúscula
            if (!password.Any(c => char.IsUpper(c)))
                return (false, null, "La contraseña debe contener al menos una letra mayúscula.");

            // Debe contener al menos una letra minúscula
            if (!password.Any(c => char.IsLower(c)))
                return (false, null, "La contraseña debe contener al menos una letra minúscula.");

            // Debe contener al menos un dígito
            if (!password.Any(c => char.IsDigit(c)))
                return (false, null, "La contraseña debe contener al menos un dígito.");

            // Debe contener al menos un carácter especial
            if (!password.Any(c => "!@#$%^&*()_+-=[]{}|;':\",.<>/?".Contains(c)))
                return (false, null, "La contraseña debe contener al menos un carácter especial.");

            // Si pasa todas las validaciones, encriptamos la contraseña
            string encrypted = EncryptPassword(password);
            return (true, encrypted, "La contraseña es segura y fue encriptada correctamente.");
        }

        // Método privado para encriptar la contraseña usando SHA256
        private string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public (bool Success, string Message, User User) RetrieveByEmailRol(string email)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var user = r.Retrieve<User>(u => u.email == email);
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







    }
}
