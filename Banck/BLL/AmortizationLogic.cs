using DAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class AmortizationLogic
    {
        public (bool Success, string Message, Amortization Amortization) CreateAmortization(Amortization amortization)
        {
            Amortization res = null;
            string message = string.Empty;
            bool success = false;

            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Create(amortization);
                    if (res != null)
                    {
                        success = true;
                        message = "Amortización creada exitosamente";
                    }
                    else
                    {
                        message = "Error al crear la amortización";
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return (success, message, res);
        }

        public (bool Success, string Message, List<Amortization> Amortizations) RetrieveAllAmortization()
        {
            List<Amortization> res = null;
            string message = string.Empty;
            bool success = false;

            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.RetrieveAll<Amortization>();
                    if (res != null && res.Count > 0)
                    {
                        success = true;
                        message = "Amortizaciones recuperadas exitosamente";
                    }
                    else
                    {
                        message = "No se encontraron amortizaciones";
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return (success, message, res);
        }

        public (bool Success, string Message, Amortization Amortization) RetrieveByIdAmortization(int id)
        {
            Amortization res = null;
            string message = string.Empty;
            bool success = false;

            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Retrieve<Amortization>(p => p.amortization_id == id);
                    if (res != null)
                    {
                        success = true;
                        message = "Amortización encontrada";
                    }
                    else
                    {
                        message = "Amortización no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return (success, message, res);
        }

        public (bool Success, string Message) Delete(int id)
        {
            bool success = false;
            string message = string.Empty;

            try
            {
                var amortization = RetrieveByIdAmortization(id).Amortization;
                if (amortization != null)
                {
                    using (var r = RepositoryFactory.CreateRepository())
                    {
                        success = r.Delete(amortization);
                        message = success ? "Amortización eliminada exitosamente" : "Error al eliminar la amortización";
                    }
                }
                else
                {
                    message = "Amortización no encontrada";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return (success, message);
        }

        public (bool Success, string Message) UpdateAmortization(Amortization amortization)
        {
            bool success = false;
            string message = string.Empty;

            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    success = r.Update(amortization);
                    message = success ? "Amortización actualizada exitosamente" : "Error al actualizar la amortización";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return (success, message);
        }
    }
}
