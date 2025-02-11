using Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SLC;
using Entities.DTOs;
using Service.Models;

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

        // Método Create para crear un nuevo usuario
        public UserCreationResponse Create(User users)
        {
            try
            {
                var response = Task.Run(async () => await SendPost<UserCreationResponse, User>("/api/User/CreateUser", users)).Result;

                if (response != null)
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
        public async Task<string> Login(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            try
            {
                var response = await SendPost<string, LoginRequest>("/api/User/Login", loginRequest);

                if (!string.IsNullOrEmpty(response))
                {
                    return response; // Mensaje del backend
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

    }
}
