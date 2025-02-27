using Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SLC;
using Entities.DTOs;

namespace Proxy
{
    public class ProxyUser : IUserService
    {
        string BaseAddress = "https://localhost:44314";

        // Método SendPost para enviar una solicitud POST
        private async Task<T> SendPost<T, PostData>(string requestURI, PostData data)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI;
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var JSONData = JsonConvert.SerializeObject(data);
                    var Response = await Client.PostAsync(requestURI, new StringContent(JSONData, Encoding.UTF8, "application/json"));

                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<T>(ResultWebAPI);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en SendPost: {ex.Message}");
                }
            }
            return Result;
        }


        // Método SendGet para enviar una solicitud GET
        private async Task<T> SendGet<T>(string requestURI)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI;
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var ResultJSON = await Client.GetStringAsync(requestURI);
                    Result = JsonConvert.DeserializeObject<T>(ResultJSON);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en SendGet: {ex.Message}");
                }
            }
            return Result;
        }


        // Método SendPut para enviar una solicitud PUT
        private async Task<T> SendPut<T, PutData>(string requestURI, PutData data)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    requestURI = BaseAddress + requestURI;
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var JSONData = JsonConvert.SerializeObject(data);
                    var Response = await Client.PutAsync(requestURI, new StringContent(JSONData, Encoding.UTF8, "application/json"));

                    var ResultWebAPI = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<T>(ResultWebAPI);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en SendPut: {ex.Message}");
                }
            }
            return Result;
        }


        // Método Create para crear un nuevo usuario
        public UserCreationResponse Create(User users)
        {
            try
            {
                var response = Task.Run(async () => await SendPost<UserCreationResponse, User>("/api/User/CreateUser", users)).Result;

                if (response != null && !string.IsNullOrEmpty(response.Message))
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo crear el usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}");
            }
        }

        // Método VerifyAndCreateUser para verificar el código y crear el usuario
        public async Task<UserCreationResponse> VerifyAndCreateUser(string email, string code)
        {
            var verifyRequest = new VerifyCodeRequest
            {
                Email = email,
                Code = code
            };

            try
            {
                // Enviar la solicitud al backend para verificar el código y crear el usuario
                var response = await SendPost<UserCreationResponse, VerifyCodeRequest>("/api/User/verifyandcreateuser", verifyRequest);

                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo verificar el código o crear el usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar el código y crear el usuario: {ex.Message}");
            }
        }

        // Método para iniciar sesión y enviar código de verificación
        public async Task<LoginResponse> Login(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            try
            {
                var response = await SendPost<LoginResponse, LoginRequest>("/api/User/Login", loginRequest);

                if (response != null && !string.IsNullOrEmpty(response.Message))
                {
                    return response;
                }
                else
                {
                    throw new Exception("Error en el inicio de sesión.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al iniciar sesión: {ex.Message}");
            }
        }


        // Método para verificar el código de autenticación
        public async Task<LoginResponse> VerifyCode(string email, string code)
        {
            var verifyRequest = new VerifyCodeRequest
            {
                Email = email,
                Code = code
            };

            try
            {
                var response = await SendPost<LoginResponse, VerifyCodeRequest>("/api/User/VerifyCode", verifyRequest);

                if (response != null)
                {
                    return response; // Devuelve el token y rol del usuario si el código es válido
                }
                else
                {
                    throw new Exception("Error al verificar el código de autenticación.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar el código: {ex.Message}");
            }
        }

        //CRUD DE USER

        public async Task<UserResponseDto> GetAllUsers()
        {
            try
            {
                var response = await SendGet<UserResponseDto>("/api/User/GetAllUsers");

                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo obtener los usuarios.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los usuarios: {ex.Message}");
            }
        }




        // Método para obtener un usuario por ID
        public UserResponseDto GetUserById(int id)
        {
            try
            {
                return Task.Run(async () => await SendGet<UserResponseDto>($"/api/User/GetUserById/{id}")).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el usuario por ID: {ex.Message}");
            }
        }


        // Método para eliminar un usuario por ID
        public ResponseDto DeleteUser(int id)
        {
            try
            {
                // Cambiar la llamada para enviar el id como parte de la URL
                var response = Task.Run(async () => await SendPost<ResponseDto, int>($"/api/User/DeleteUser/{id}", id)).Result;

                if (response != null)
                {
                    return response; // Mensaje de éxito
                }
                else
                {
                    throw new Exception("No se pudo eliminar el usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el usuario: {ex.Message}");
            }
        }



        // Método para actualizar un usuario por ID
        public ResponseDto UpdateUser(int id, User updatedUser)
        {
            try
            {
                var response = Task.Run(async () => await SendPut<ResponseDto, User>($"/api/User/UpdateUser/{id}", updatedUser)).Result;

                if (response != null)
                {
                    return response; // Mensaje de éxito
                }
                else
                {
                    throw new Exception("No se pudo actualizar el usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario: {ex.Message}");
            }
        }

        // *** Nuevo método para crear el usuario de forma directa (sin verificación) ***
        public async Task<UserCreationResponse> DirectCreateUser(User user)
        {
            try
            {
                user.status = 1;
                user.registration_date = DateTime.Now;

                var response = await SendPost<UserCreationResponse, User>("/api/user/directcreate", user);
                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo crear el usuario directamente.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario directamente: {ex.Message}");
            }
        }

        // Método para solicitar el envío del código de recuperación de contraseña
        public async Task<ResponseDto> RecoverPassword(string email)
        {
            try
            {
                // Enviar un objeto con la propiedad Email
                var requestData = new EmailRequestDto { Email = email };
                var response = await SendPost<ResponseDto, EmailRequestDto>("/api/user/recoverpassword", requestData);
                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo enviar el código de recuperación.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al solicitar recuperación de contraseña: {ex.Message}");
            }
        }


        // Método para confirmar el código y restablecer la contraseña
        public async Task<ResponseDto> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var response = await SendPost<ResponseDto, ResetPasswordRequest>("/api/user/resetpassword", request);
                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo restablecer la contraseña.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al restablecer la contraseña: {ex.Message}");
            }
        }







    }
}
