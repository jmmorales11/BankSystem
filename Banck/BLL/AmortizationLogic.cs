using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
                decimal monthlyInterestRate = loan.interest_rate / 100 / 12;
                decimal monthlyPayment = remainingPrincipal * monthlyInterestRate *
                                         (decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments) /
                                         ((decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments) - 1);

                DateTime startDate = loan.application_date ?? DateTime.Now;

                for (int i = 1; i <= totalPayments; i++)
                {
                    decimal interestForPeriod = remainingPrincipal * monthlyInterestRate;
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

        public (bool Success, string Message, Amortization Amortization) PayInstallment(int amortizationId)
        {
            // Recuperamos la cuota usando el método existente
            var retrieveResult = RetrieveByIdAmortization(amortizationId);
            if (!retrieveResult.Success || retrieveResult.Amortization == null)
            {
                return (false, "Amortización no encontrada.", null);
            }

            var installment = retrieveResult.Amortization;

            // Si ya se ha registrado la fecha de pago, retornamos esa información
            if (installment.payment_date.HasValue)
            {
                return (true, "La cuota ya fue pagada.", installment);
            }

            // Actualizamos la fecha de pago a la fecha actual
            installment.payment_date = DateTime.Now;

            // Actualizamos la cuota en la base de datos
            var updateResult = UpdateAmortization(installment);
            if (!updateResult.Success)
            {
                return (false, updateResult.Message, null);
            }

            // Verificamos si se han pagado todas las cuotas del préstamo
            // Primero, obtenemos todas las cuotas del préstamo asociado
            var allScheduleResult = RetrieveAllAmortization();
            if (!allScheduleResult.Success || allScheduleResult.Amortizations == null)
            {
                return (false, "No se pudieron recuperar las cuotas del préstamo.", installment);
            }

            // Filtramos las cuotas del préstamo actual
            var schedule = allScheduleResult.Amortizations.Where(a => a.loan_id == installment.loan_id).ToList();
            // Contamos las cuotas pagadas
            int cuotasPagadas = schedule.Count(a => a.payment_date.HasValue);
            int totalCuotas = schedule.Count;

            // Si todas las cuotas están pagadas, actualizamos el estado del préstamo a inactivo
            if (cuotasPagadas == totalCuotas)
            {
                // Supongamos que LoanLogic tiene un método para actualizar el préstamo
                var loanLogic = new LoanLogic();
                // Recuperamos el préstamo
                var loanResult = loanLogic.RetrieveByIdLoan(installment.loan_id);
                if (loanResult.Success && loanResult.Loan != null)
                {
                    var loan = loanResult.Loan;
                    // Cambiamos el estado a inactivo (por ejemplo, 0)
                    loan.status = 0;
                    var updateLoanResult = loanLogic.UpdateLoan(loan);
                    if (updateLoanResult.Success)
                    {
                        // Puedes agregar un mensaje indicando que el préstamo se cerró
                        updateResult.Message += " Además, el préstamo ha sido finalizado.";
                    }
                    else
                    {
                        // Si falla la actualización, se puede registrar el error, pero no se detiene el proceso
                        updateResult.Message += " Sin embargo, no se pudo actualizar el estado del préstamo.";
                    }
                }
            }

            return (true, "Cuota pagada exitosamente. " + updateResult.Message, installment);
        }




    }
}
