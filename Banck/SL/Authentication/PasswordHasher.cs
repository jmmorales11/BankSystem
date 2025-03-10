﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Authentication
{
    public class PasswordHasher
    {
        // Método para generar el hash de una contraseña
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Método para verificar si una contraseña coincide con el hash
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
