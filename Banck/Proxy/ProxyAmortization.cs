using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;  // Asegúrate de tener definido un DTO para la respuesta
using Entities;

namespace Proxy
{
    public class ProxyAmortization
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

        // Método para obtener el cronograma de amortización de un préstamo específico
        public async Task<AmortizationResponseDto> GetLoanAmortizationSchedule(int loanId)
        {
            try
            {
                var response = await SendGet<AmortizationResponseDto>($"/api/loan/{loanId}/amortization");

                if (response != null && response.Success)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se encontró cronograma de amortización para el préstamo solicitado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el cronograma de amortización: {ex.Message}");
            }
        }

        // Nuevo método para pagar una cuota
        public async Task<ResponseDto> PayInstallment(int amortizationId)
        {
            try
            {
                // Usamos SendPost; se envía el ID en el body (aunque el endpoint lo obtiene de la URL)
                var response = await SendPost<ResponseDto, int>($"/api/amortization/pay/{amortizationId}", amortizationId);
                if (response != null)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No se pudo procesar el pago de la cuota.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al pagar la cuota: {ex.Message}");
            }
        }
    }
}
