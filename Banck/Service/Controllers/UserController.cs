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
using log4net;

namespace Service.Controllers
{
    public class UserController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));
        private static Dictionary<string, string> VerificationCodes = new Dictionary<string, string>();
        private static Dictionary<string, string> RecoveryCodes = new Dictionary<string, string>();

        private readonly IEmailService _emailService;

        public UserController()
        {
            _emailService = new EmailService();  // Inicialización manual
        }

        ///REGISTRAR USUARIOS

        private static Dictionary<string, User> PendingRegistrations = new Dictionary<string, User>();



        [AllowAnonymous]

        [HttpPost]
        public async Task<IHttpActionResult> CreateUser([FromBody] User newUser)
        {
            var BL = new UserLogic();

            try
            {
                // Verificar si el usuario ya existe
                if (BL.validateExistingUser(newUser))
                {
                    log.Warn($"Intento de registro con un usuario ya existente: {newUser.email}");
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = "Este usuario ya existe."
                    });
                }

                // Validar la contraseña y encriptarla
                var passwordResult = BL.ValidateAndEncryptPassword(newUser.password);
                if (!passwordResult.IsSafe)
                {
                    log.Warn($"Intento de registro con una contraseña insegura para el usuario: {newUser.email}");
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = passwordResult.Message
                    });
                }
                // Reemplazar la contraseña por su versión encriptada
                newUser.password = passwordResult.EncryptedPassword;

                // Generar y enviar el código de verificación
                string verificationCode = new Random().Next(100000, 999999).ToString();

                // Guardar el código y los datos del usuario en memoria
                VerificationCodes[newUser.email] = verificationCode;
                PendingRegistrations[newUser.email] = newUser;

                // Enviar el código por correo electrónico
                string subject = "Código de verificación para registro";
                string body = $"Tu código de verificación es: <strong>{verificationCode}</strong>";
                await _emailService.SendEmailAsync(newUser.email, subject, body);
                
                log.Info($"Código de verificación enviado a: {newUser.email}");

                // Crear la respuesta con el mensaje y el usuario (pendiente de verificación)
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
                log.Error("Error al crear el usuario", ex);
                return Content(HttpStatusCode.InternalServerError, new
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado. Inténtalo de nuevo."
                });
            }
        }



        //Verifica el código que se envió
        [AllowAnonymous]
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

                    log.Info($"Usuario creado exitosamente: {newUser.email}");

                    return Ok(new { Message = "Usuario creado exitosamente.", User = createdUser });
                }
                else
                {
                    log.Warn($"No se encontraron datos de registro para el correo: {verifyRequest.Email}");
                    return Content(HttpStatusCode.NotFound, new { Message = "No se encontraron datos de registro para este correo." });
                }
            }
            else
            {
                log.Warn($"Código de verificación inválido o expirado para el correo: {verifyRequest.Email}");
                return Content(HttpStatusCode.Unauthorized, new { Message = "Código de verificación inválido o expirado." });
            }
        }

        /// INICIAR SESION
        [AllowAnonymous]
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
                    log.Warn($"Intento de inicio de sesión fallido para el usuario: {loginRequest.Email}");
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
                
                log.Info($"Código de verificación enviado a: {loginRequest.Email}");

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
                log.Error("Error en el inicio de sesión", ex);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = "Error en el servidor: " + ex.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult VerifyCode([FromBody] VerifyCodeRequest verifyRequest)
        {
            if (VerificationCodes.TryGetValue(verifyRequest.Email, out string storedCode) && storedCode == verifyRequest.Code)
            {
                // Eliminar el código una vez validado (opcional)
                VerificationCodes.Remove(verifyRequest.Email);

                // Recuperar el usuario real mediante el correo
                var userLogic = new UserLogic();
                var (success, message, user) = userLogic.RetrieveByEmailRol(verifyRequest.Email);
                if (!success)
                {
                    log.Warn($"No se encontró el usuario con el correo: {verifyRequest.Email}");
                    return Content(HttpStatusCode.NotFound, new { Message = message });
                }

                // Extraer el rol real del usuario
                string userRole = user.role;

                // Generar el token JWT con el email y el rol real
                var token = JwtService.GenerateToken(verifyRequest.Email, userRole, user.user_id);
                log.Info($"Inicio de sesión exitoso para el usuario: {verifyRequest.Email}");

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
                log.Warn($"Código de verificación inválido o expirado para el correo: {verifyRequest.Email}");
                return Content(HttpStatusCode.Unauthorized, new { Message = "Código de verificación inválido o expirado." });
            }
        }



        ///CRUD DE USUARIOS
        ///
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var userLogic = new UserLogic();
            var (success, message, users) = userLogic.RetrieveAllUser();

            if (success)
            {
                log.Info("Usuarios recuperados exitosamente.");
                return Ok(new { Success = true, Users = users });
            }
            else
            {
                log.Error("Error al recuperar los usuarios: " + message);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            var userLogic = new UserLogic();
            var (success, message, user) = userLogic.RetrieveByIdUser(id);

            if (success)
            {
                log.Info($"Usuario recuperado exitosamente: {id}");
                return Ok(new { Success = true, User = user });
            }
            else
            {
                log.Warn($"No se encontró el usuario con el ID: {id}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] 
        public IHttpActionResult DeleteUser(int id)
        {
            var userLogic = new UserLogic();
            var (success, message) = userLogic.Delete(id);

            if (success)
            {
                log.Info($"Usuario eliminado exitosamente: {id}");
                return Ok(new { Success = true, Message = message });
            }
            else
            {
                log.Warn($"No se pudo eliminar el usuario con el ID: {id}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = message });
            }
        }


        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var userLogic = new UserLogic();

            try
            {
                // Verificar si el usuario existe
                var (successRetrieve, messageRetrieve, existingUser) = userLogic.RetrieveByIdUser(id);
                if (!successRetrieve)
                {
                    log.Warn($"No se encontró el usuario con el ID: {id}");
                    return Content(HttpStatusCode.NotFound, new { Success = false, Message = messageRetrieve });
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
                var (successUpdate, messageUpdate) = userLogic.UpdateUser(existingUser);

                if (!successUpdate)
                {
                    log.Error($"Error al actualizar el usuario con el ID: {id} - {messageUpdate}");
                    return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = messageUpdate });
                }

                return Ok(new { Success = true, Message = messageUpdate, User = existingUser });
            }
            catch (Exception ex)
            {
                log.Error($"Error en el servidor al actualizar el usuario con el ID: {id}", ex);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = "Error en el servidor: " + ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/user/directcreate")]
        public IHttpActionResult DirectCreateUser([FromBody] User newUser)
        {
            var BL = new UserLogic();
            try
            {
                // Verificar si el usuario ya existe
                if (BL.validateExistingUser(newUser))
                {
                    log.Warn($"Intento de registro directo con un usuario ya existente: {newUser.email}");
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = "Este usuario ya existe."
                    });
                }

                // Validar la contraseña y encriptarla
                var passwordResult = BL.ValidateAndEncryptPassword(newUser.password);
                if (!passwordResult.IsSafe)
                {
                    log.Warn($"Intento de registro directo con una contraseña insegura para el usuario: {newUser.email}");
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = passwordResult.Message
                    });
                }
                // Reemplazar la contraseña por su versión encriptada
                newUser.password = passwordResult.EncryptedPassword;

                newUser.status = 1;
                newUser.registration_date = DateTime.Now;

                // Crear el usuario directamente
                var createdUser = BL.CreateUser(newUser);
                log.Info($"Usuario creado directamente: {newUser.email}");
                return Ok(new { Success = true, Message = "Usuario creado exitosamente.", User = createdUser });
            }
            catch (Exception ex)
            {
                log.Error("Error al crear el usuario directamente", ex);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = "Error: " + ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/directcreate-two")]
        public IHttpActionResult DirectCreateUser2([FromBody] User newUser)
        {
            var BL = new UserLogic();
            try
            {
                // Verificar si el usuario ya existe
                if (BL.validateExistingUser(newUser))
                {
                    log.Warn($"Intento de registro directo con un usuario ya existente: {newUser.email}");
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = "Este usuario ya existe."
                    });
                }

                // Validar la contraseña y encriptarla
                var passwordResult = BL.ValidateAndEncryptPassword(newUser.password);
                if (!passwordResult.IsSafe)
                {
                    log.Warn($"Intento de registro directo con una contraseña insegura para el usuario: {newUser.email}");
                    return Content(HttpStatusCode.BadRequest, new
                    {
                        Success = false,
                        Message = passwordResult.Message
                    });
                }
                // Reemplazar la contraseña por su versión encriptada
                newUser.password = passwordResult.EncryptedPassword;

                newUser.status = 1;
                newUser.registration_date = DateTime.Now;

                // Crear el usuario directamente
                var createdUser = BL.CreateUser(newUser);
                log.Info($"Usuario creado directamente: {newUser.email}");
                return Ok(new { Success = true, Message = "Usuario creado exitosamente.", User = createdUser });
            }
            catch (Exception ex)
            {
                log.Error("Error al crear el usuario directamente", ex);
                return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = "Error: " + ex.Message });
            }
        }


        // RECUPERAR CONTRASEÑA: Envía el código de recuperación
        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/recoverpassword")]
        public async Task<IHttpActionResult> RecoverPassword([FromBody] EmailRequest request)
        {
            var userLogic = new UserLogic();
            var user = userLogic.RetrieveByEmail(request.Email);
            if (user == null)
            {
                log.Warn($"Intento de recuperación de contraseña para un usuario no encontrado: {request.Email}");
                return Content(HttpStatusCode.NotFound, new { Success = false, Message = "Usuario no encontrado" });
            }

            string recoveryCode = new Random().Next(100000, 999999).ToString();
            RecoveryCodes[request.Email] = recoveryCode;

            string subject = "Código de recuperación de contraseña";
            string body = $"Tu código de recuperación es: <strong>{recoveryCode}</strong>";
            await _emailService.SendEmailAsync(request.Email, subject, body);
            log.Info($"Código de recuperación enviado a: {request.Email}");
            return Ok(new { Success = true, Message = "Se ha enviado un código de recuperación al correo." });
        }

        // RESET PASSWORD: Verifica el código y actualiza la contraseña (después de validarla y encriptarla)
        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/resetpassword")]
        public IHttpActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (RecoveryCodes.TryGetValue(request.Email, out string storedCode) && storedCode == request.Code)
            {
                // Una vez validado el código, lo eliminamos para evitar reusos
                RecoveryCodes.Remove(request.Email);

                var userLogic = new UserLogic();
                // Validar y encriptar la nueva contraseña
                var passwordValidation = userLogic.ValidateAndEncryptPassword(request.NewPassword);
                if (!passwordValidation.IsSafe)
                {
                    log.Warn($"Intento de reseteo de contraseña: {request.Email}");
                    // Si la contraseña no es segura, se retorna un error
                    return Content(HttpStatusCode.BadRequest, new { Success = false, Message = passwordValidation.Message });
                }

                // Se actualiza la contraseña usando la versión encriptada
                var (success, message) = userLogic.ResetPassword(request.Email, passwordValidation.EncryptedPassword);
                if (success)
                {
                    log.Info($"Contraseña reseteada exitosamente para el usuario: {request.Email}");
                    return Ok(new { Success = true, Message = message });
                }
                else
                {
                    log.Error($"Error al resetear la contraseña para el usuario: {request.Email} - {message}");
                    return Content(HttpStatusCode.InternalServerError, new { Success = false, Message = message });
                }
            }
            else
            {
                log.Warn($"Código de recuperación inválido o expirado para el usuario: {request.Email}");
                return Content(HttpStatusCode.Unauthorized, new { Success = false, Message = "Código de recuperación inválido o expirado." });
            }
        }



    }
}
