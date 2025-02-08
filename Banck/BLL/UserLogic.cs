using System;
using System.Collections.Generic;
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

        public void validateExistingUserAndPasswordSecure(User user)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                User existingUser = r.Retrieve<User>(u => u.email == user.email);
                if(existingUser != null)
                {
                    throw new Exception("This user already exists");
                }
                if(!ValidatePassword(user, user.password))
                {
                    throw new Exception("This password is not secure");
                }
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

        private bool ValidatePassword(User user, string password)
        {
            // Verificar longitud mínima
            if (password.Length < 13)
                return false;

            // Verificar que contenga al menos un carácter especial
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?\:{ }|<>]"))
                return false;

            // Verificar que no contenga partes del nombre de usuario o correo electrónico
            var userNameParts = user.first_name.Split(new char[] { ' ', '.', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in userNameParts)
            {
                if (password.Contains(part))
                    return false;
            }

            // Verificar que no contenga partes del correo electrónico
            var emailParts = user.email.Split(new char[] { '@', '.', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in emailParts)
            {
                if (password.Contains(part))
                    return false;
            }

            return true;
        }



    }
}
