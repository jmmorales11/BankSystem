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

        public (bool Success, string Message, List<Amortization> Schedule) GenerateAndSaveSchedule(Loan loan)
        {
            try
            {
                List<Amortization> schedule = new List<Amortization>();

                decimal remainingPrincipal = loan.requested_amount;
                int totalPayments = loan.payment_term_months;
                // Convertir tasa de interés anual a tasa mensual (porcentaje a decimal)
                decimal monthlyInterestRate = loan.interest_rate / 100 / 12;
                // Calcular el pago mensual usando la fórmula de amortización
                decimal monthlyPayment = remainingPrincipal * monthlyInterestRate *
                                         (decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments) /
                                         ((decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments) - 1);

                // Utilizamos la fecha de aplicación del préstamo o DateTime.Now si es null
                DateTime startDate = loan.application_date ?? DateTime.Now;

                for (int i = 1; i <= totalPayments; i++)
                {
                    // Interés del periodo actual
                    decimal interestForPeriod = remainingPrincipal * monthlyInterestRate;
                    // Parte del principal
                    decimal principalPayment = monthlyPayment - interestForPeriod;

                    Amortization amortizationRecord = new Amortization
                    {
                        installment_number = i,
                        due_date = startDate.AddMonths(i),
                        payment_amount = decimal.Round(monthlyPayment, 2),
                        principal = decimal.Round(principalPayment, 2),
                        remaining_balance = decimal.Round(remainingPrincipal - principalPayment, 2),
                        loan_id = loan.loan_id
                    };

                    schedule.Add(amortizationRecord);
                    remainingPrincipal -= principalPayment;
                    if (remainingPrincipal < 0)
                        remainingPrincipal = 0;
                }

                // Persistir la tabla de amortización en la base de datos
                using (var r = RepositoryFactory.CreateRepository())
                {
                    foreach (var amortization in schedule)
                    {
                        r.Create(amortization);
                    }
                }

                return (true, "Cronograma de amortización generado y guardado exitosamente.", schedule);
            }
            catch (Exception ex)
            {
                return (false, $"Error al generar el cronograma: {ex.Message}", null);
            }
        }


    }
}
