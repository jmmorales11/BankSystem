using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Entities;

namespace Proxy
{
    public class ProxyLoan
    {
        string BaseAddress = "https://localhost:44314";
        private readonly string _token;

        public ProxyLoan(string token = null)
        {
            _token = token;
        }

        // Método SendPost para enviar una solicitud POST
        private async Task<T> SendPost<T, PostData>(string requestURI, PostData data)
        {
            T Result = default(T);
            using (var Client = new HttpClient())
            {
                try
                {
                    // <<< Agregamos esta parte para la cabecera Bearer >>>
                    if (!string.IsNullOrEmpty(_token))
                    {
                        Client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", _token);
                    }
                    // <<< fin de agregado >>>
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
                    // <<< Agregamos esta parte para la cabecera Bearer >>>
                    if (!string.IsNullOrEmpty(_token))
                    {
                        Client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", _token);
                    }
                    // <<< fin de agregado >>>
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
                    // <<< Agregamos esta parte para la cabecera Bearer >>>
                    if (!string.IsNullOrEmpty(_token))
                    {
                        Client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", _token);
                    }
                    // <<< fin de agregado >>>
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



        //CREAR UN PRÉSTAMO
        public async Task<ResponseDto> CreateLoan(Loan loan)
        {
            try
            {
                var response = await SendPost<ResponseDto, Loan>("/api/Loan/CreateLoan", loan);

                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo crear los datos del prestamos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear los datos del prestamo: {ex.Message}");
            }
        }

        // OBTENER TODOS LOS PRESTAMOS
        public async Task<LoanResponseDto> GetAllLoan()
        {
            try
            {
                var response = await SendGet<LoanResponseDto>("/api/Loan/GetAllLoans");

                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo obtener los datos del préstamo");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los datos del préstamo: {ex.Message}");
            }
        }

        // BUSCAR PRESTAMOS POR ID DEL USUARIO
        public async Task<LoanResponseDto> GetLoanById(int id)
        {
            try
            {
                // Realizar la solicitud GET
                var response = await SendGet<LoanResponseDto>($"/api/Loan/GetLoanById/{id}");

                // Verificar si la respuesta es nula o no contiene datos
                if (response == null || response.Loan == null)
                {
                    // Si no se encontraron datos, devolver Success: false
                    return new LoanResponseDto
                    {
                        Success = false,
                        Message = "No se encontraron datos para el prestamo solicitado."
                    };
                }

                // Si los datos están presentes, devolver Success: true
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, capturar la excepción y devolver Success: false
                return new LoanResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener los datos del prestamo por ID: {ex.Message}"
                };
            }
        }

        // Método para eliminar los datos de un usuario por ID (ahora asincrónico)
        public async Task<ResponseDto> DeleteUserData(int id)
        {
            try
            {
                return await SendPost<ResponseDto, int>($"/api/Loan/DeleteLoan/{id}", id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar los datos del prestamo: {ex.Message}");
            }
        }


        //ACTUALIZAR PRESTAMO
        public async Task<LoanResponseDto> UpdateUserData(int id, Loan loan)
        {
            try
            {
                // Realizar la solicitud PUT para actualizar
                var response = await SendPut<LoanResponseDto, Loan>($"/api/Loan/UpdateLoan/{id}", loan);

                // Verificar si la respuesta es nula o no contiene datos
                if (response == null || !response.Success)
                {
                    return new LoanResponseDto
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
                return new LoanResponseDto
                {
                    Success = false,
                    Message = $"Error al actualizar los datos del prestamo: {ex.Message}"
                };
            }
        }

        // OBTENER PRESTAMOS POR ID DEL USUARIO
        public async Task<LoanResponseDto> GetLoansByUserId(int userId)
        {
            try
            {
                // Realizar la solicitud GET
                var response = await SendGet<LoanResponseDto>($"/api/Loan/GetLoansByUserId/{userId}");

                // Verificar si la respuesta es nula o no contiene datos
                if (response == null || response.Loans == null || !response.Loans.Any())
                {
                    // Si no se encontraron datos, devolver Success: false
                    return new LoanResponseDto
                    {
                        Success = false,
                        Message = "No se encontraron préstamos para el usuario solicitado."
                    };
                }

                // Si los datos están presentes, devolver Success: true
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                // En caso de error, capturar la excepción y devolver Success: false
                return new LoanResponseDto
                {
                    Success = false,
                    Message = $"Error al obtener los préstamos del usuario: {ex.Message}"
                };
            }
        }





    }
}
