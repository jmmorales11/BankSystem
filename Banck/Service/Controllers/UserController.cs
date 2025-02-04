using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using BLL;
using SLC;
using Service.Models;
using System.Threading.Tasks;
using SL.Authentication;
using SL.Authorization;

namespace Service.Controllers
{
    public class UserController : ApiController
    {
        private static Dictionary<string, string> VerificationCodes = new Dictionary<string, string>();
        private readonly IEmailService _emailService;

        public UserController()
        {
            _emailService = new EmailService();  // Inicialización manual
        }

        ///REGISTRAR USUARIOS

        private static Dictionary<string, User> PendingRegistrations = new Dictionary<string, User>();

        [HttpPost]
        public async Task<IHttpActionResult> CreateUser([FromBody] User newUser)
        {
            var BL = new UserLogic();

            // Verificar si el usuario ya existe
            //var existingUser = BL.GetUserByEmail(newUser.Email);
            //if (existingUser != null)
            //{
            //    return Content(HttpStatusCode.Conflict, new { Message = "El usuario ya existe." });
            //}

            // Generar código de verificación
            string verificationCode = new Random().Next(100000, 999999).ToString();

            // Guardar el código y los datos del usuario en memoria
            VerificationCodes[newUser.email] = verificationCode;
            PendingRegistrations[newUser.email] = newUser;

            // Enviar el código por correo electrónico
            string subject = "Código de verificación para registro";
            string body = $"Tu código de verificación es: <strong>{verificationCode}</strong>";
            await _emailService.SendEmailAsync(newUser.email, subject, body);

            return Ok(new { Message = "Se ha enviado un código de verificación al correo. Ingresa el código para completar el registro." });
        }

        [HttpPost]
        public IHttpActionResult VerifyAndCreateUser([FromBody] VerifyCodeRequest verifyRequest)
        {
            var BL = new UserLogic();

            // Verificar el código de verificación
            if (VerificationCodes.TryGetValue(verifyRequest.Email, out string storedCode) && storedCode == verifyRequest.Code)
            {
                // Eliminar el código una vez validado
                VerificationCodes.Remove(verifyRequest.Email);

                // Recuperar los datos del usuario pendiente de registro
                if (PendingRegistrations.TryGetValue(verifyRequest.Email, out User newUser))
                {
                    // Crear el usuario en la base de datos
                    var createdUser = BL.Create(newUser);

                    // Eliminar los datos pendientes de registro
                    PendingRegistrations.Remove(verifyRequest.Email);

                    return Ok(new { Message = "Usuario creado exitosamente.", User = createdUser });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "No se encontraron datos de registro para este correo." });
                }
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Código de verificación inválido o expirado." });
            }
        }

        ///INICIAR SESION

        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var BL = new UserLogic();
            string recipientEmail = loginRequest.Email;
            string subject1 = "Código de verificación";
            string body1;

            try
            {
                // Intentar autenticar al usuario
                var user = BL.Authenticate(loginRequest.Email, loginRequest.Password);

                if (user == null)
                {
                    return Content(HttpStatusCode.Unauthorized, new { Message = "Usuario no encontrado" });
                }

                // Generar código de verificación
                string verificationCode = new Random().Next(100000, 999999).ToString();

                // Guardar el código en memoria
                VerificationCodes[loginRequest.Email] = verificationCode;

                // Enviar el código por correo electrónico
                body1 = $"Tu código de verificación es: <strong>{verificationCode}</strong>";
                await _emailService.SendEmailAsync(recipientEmail, subject1, body1);

                return Ok(new { Message = "Se ha enviado un código al correo. Ingresa el código para completar el inicio de sesión." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Endpoint para verificar el código de autenticación
        [HttpPost]
        public IHttpActionResult VerifyCode([FromBody] VerifyCodeRequest verifyRequest)
        {
            if (VerificationCodes.TryGetValue(verifyRequest.Email, out string storedCode) && storedCode == verifyRequest.Code)
            {
                // Eliminar el código una vez validado si lo deseas
                // VerificationCodes.Remove(verifyRequest.Email);

                // Para el ejemplo, asignamos un rol predeterminado
                string userRole = "User";

                // Generar token JWT
                var token = JwtService.GenerateToken(verifyRequest.Email, userRole);

                return Ok(new
                {
                    Token = token,
                    Email = verifyRequest.Email,
                    Role = userRole,
                    Message = "Login exitoso"
                });
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Código de verificación inválido o expirado." });
            }
        }
    }
}
