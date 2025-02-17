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
using Entities.DTOs;

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


        //Envia el codigo de verificación
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser([FromBody] User newUser)
        {
            var BL = new UserLogic();

            try
            {
                // Verificar si el usuario ya existe
                if (BL.validateExistingUser(newUser))
                {
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = "Este usuario ya existe."
                    });
                }

                string verificationCode = new Random().Next(100000, 999999).ToString();

                // Guardar el código y los datos del usuario en memoria
                VerificationCodes[newUser.email] = verificationCode;
                PendingRegistrations[newUser.email] = newUser;

                // Enviar el código por correo electrónico
                string subject = "Código de verificación para registro";
                string body = $"Tu código de verificación es: <strong>{verificationCode}</strong>";
                await _emailService.SendEmailAsync(newUser.email, subject, body);

                // Crear la respuesta con el mensaje y el usuario
                var response = new UserCreationResponse
                {
                    User = newUser,
                    Success = true,
                    Message = "Se ha enviado un código de verificación al correo. Ingresa el código para completar el registro."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado. Inténtalo de nuevo."
                });
            }
        }


        //Verifica el código que se envió
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

        /// INICIAR SESION
        [HttpPost]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var BL = new UserLogic();

            try
            {
                // Intentar autenticar al usuario
                var user = BL.Authenticate(loginRequest.Email, loginRequest.Password);

                if (user == null)
                {
                    return Content(HttpStatusCode.Unauthorized, new { Success = false, Message = "Credenciales incorrectas." });
                }

                // Generar código de verificación
                string verificationCode = new Random().Next(100000, 999999).ToString();

                // Guardar el código en memoria
                VerificationCodes[loginRequest.Email] = verificationCode;

                // Enviar el código por correo electrónico
                string subject = "Código de verificación";
                string body = $"Tu código de verificación es: <strong>{verificationCode}</strong>";
                await _emailService.SendEmailAsync(loginRequest.Email, subject, body);

                // Devolver el correo y el mensaje
                return Ok(new
                {
                    Success = true,
                    Email = loginRequest.Email,
                    Message = "Se ha enviado un código al correo. Ingresa el código para completar el inicio de sesión."
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = "Error en el servidor: " + ex.Message });
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


        ///CRUD DE USUARIOS
        ///

        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var BL = new UserLogic();
                var users = BL.RetrieveAllUser();
                if (users == null || !users.Any())
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "No se encontraron usuarios." });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor: " + ex.Message });
            }
        }

        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var BL = new UserLogic();
                var user = BL.RetrieveByIdUser(id);
                if (user == null)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Usuario no encontrado." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor: " + ex.Message });
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                var BL = new UserLogic();
                bool deleted = BL.Delete(id);
                if (!deleted)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "No se pudo eliminar el usuario. Puede que no exista." });
                }

                return Ok(new { Message = "Usuario eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor: " + ex.Message });
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var BL = new UserLogic();

            try
            {
                // Verificar si el usuario existe
                var existingUser = BL.RetrieveByIdUser(id);
                if (existingUser == null)
                {
                    return Content(HttpStatusCode.NotFound, new { Message = "Usuario no encontrado." });
                }

                // Actualizar los campos del usuario
                existingUser.first_name = updatedUser.first_name ?? existingUser.first_name;
                existingUser.last_name = updatedUser.last_name ?? existingUser.last_name;
                existingUser.birth_date = updatedUser.birth_date != default(DateTime) ? updatedUser.birth_date : existingUser.birth_date;
                existingUser.address = updatedUser.address ?? existingUser.address;
                existingUser.email = updatedUser.email ?? existingUser.email;
                existingUser.role = updatedUser.role ?? existingUser.role;
                existingUser.password = updatedUser.password ?? existingUser.password;
                existingUser.registration_date = updatedUser.registration_date ?? existingUser.registration_date;
                existingUser.account_locked_until = updatedUser.account_locked_until ?? existingUser.account_locked_until;
                existingUser.failed_login_attempts = updatedUser.failed_login_attempts ?? existingUser.failed_login_attempts;
                existingUser.status = updatedUser.status ?? existingUser.status;

                // Llamar al método de lógica para realizar la actualización
                bool updated = BL.UpdateUser(existingUser);

                if (!updated)
                {
                    return Content(HttpStatusCode.InternalServerError, new { Message = "No se pudo actualizar el usuario." });
                }

                return Ok(new { Message = "Usuario actualizado exitosamente.", User = existingUser });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Error en el servidor: " + ex.Message });
            }
        }


    }
}
