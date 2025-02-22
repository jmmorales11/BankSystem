using Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Proxy
{
    public class ProxyUserData
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

        // Método para crear datos de usuario
        public async Task<ResponseDto> CreateUserData(User_Data userData)
        {
            try
            {
                var response = await SendPost<ResponseDto, User_Data>("/api/UserData/CreateUserData", userData);

                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo crear los datos del usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear los datos del usuario: {ex.Message}");
            }
        }

        // Método para obtener todos los datos de usuario
        public async Task<UserDataResponseDto> GetAllUserData()
        {
            try
            {
                var response = await SendGet<UserDataResponseDto>("/api/UserData/GetAllUserData");

                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo obtener los datos del usuario.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los datos del usuario: {ex.Message}");
            }
        }

        // Método para obtener datos 
        public async Task<UserDataResponseDto> GetUserDataById(int id)
        {
            try
            {
                // Realizar la solicitud GET
                var response = await SendGet<UserDataResponseDto>($"/api/UserData/GetUserDataById/{id}");

                // Verificar si la respuesta es nula o no contiene datos
                if (response == null || response.UserData == null)
                {
                    // Si no se encontraron datos, devolver Success: false
                    return new UserDataResponseDto
                    {
                        Success = false,
                        Message = "No se encontraron datos para el usuario solicitado."
                    };
                }

                // Si los datos están presentes, devolver Success: true
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, capturar la excepción y devolver Success: false
                return new UserDataResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener los datos del usuario por ID: {ex.Message}"
                };
            }
        }



        // Método para eliminar los datos de un usuario por ID (ahora asincrónico)
        public async Task<ResponseDto> DeleteUserData(int id)
        {
            try
            {
                return await SendPost<ResponseDto, int>($"/api/UserData/DeleteUserData/{id}", id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar los datos del usuario: {ex.Message}");
            }
        }


        public async Task<UserDataResponseDto> UpdateUserData(int id, User_Data userData)
        {
            try
            {
                // Realizar la solicitud PUT para actualizar
                var response = await SendPut<UserDataResponseDto, User_Data>($"/api/UserData/UpdateUserData/{id}", userData);

                // Verificar si la respuesta es nula o no contiene datos
                if (response == null || !response.Success)
                {
                    return new UserDataResponseDto
                    {
                        Success = false,
                        Message = "Error al actualizar los datos."
                    };
                }

                // Si los datos se actualizaron correctamente, devolver Success: true
                return response;
            }
            catch (Exception ex)
            {
                return new UserDataResponseDto
                {
                    Success = false,
                    Message = $"Error al actualizar los datos del usuario: {ex.Message}"
                };
            }
        }


    }
}
