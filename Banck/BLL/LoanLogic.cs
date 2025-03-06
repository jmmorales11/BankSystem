using DAL;
using Entities;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class LoanLogic
    {
        public (bool Success, string Message, Loan CreatedLoan) Create(Loan loan)
        {
            // Validar la elegibilidad del usuario para el préstamo
            var eligibility = ValidateLoanEligibility(loan);
            if (!eligibility.IsEligible)
            {
                return (false, eligibility.Message, null);
            }

            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    loan.application_date = DateTime.Now;
                    var createdLoan = r.Create(loan);
                    return (true, "Préstamo creado exitosamente", createdLoan);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al crear el préstamo: {ex.Message}", null);
            }
        }


        public (bool Success, string Message, List<Loan> Loans) RetrieveAllLoan()
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var loans = r.RetrieveAll<Loan>();
                    return (true, "Préstamos recuperados exitosamente", loans);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar los préstamos: {ex.Message}", null);
            }
        }

        public (bool Success, string Message, Loan Loan) RetrieveByIdLoan(int id)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var loan = r.Retrieve<Loan>(p => p.loan_id == id);
                    if (loan == null)
                    {
                        return (false, "Préstamo no encontrado", null);
                    }
                    return (true, "Préstamo encontrado", loan);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar el préstamo: {ex.Message}", null);
            }
        }

        public (bool Success, string Message) Delete(int id)
        {
            try
            {
                var loan = RetrieveByIdLoan(id).Loan;
                if (loan == null)
                {
                    return (false, "Préstamo no encontrado");
                }

                using (var r = RepositoryFactory.CreateRepository())
                {
                    bool deleted = r.Delete(loan);
                    return deleted
                        ? (true, "Préstamo eliminado exitosamente")
                        : (false, "No se pudo eliminar el préstamo");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar el préstamo: {ex.Message}");
            }
        }

        public (bool Success, string Message) UpdateLoan(Loan loan)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    bool updated = r.Update(loan);
                    return updated
                        ? (true, "Préstamo actualizado exitosamente")
                        : (false, "No se pudo actualizar el préstamo");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar el préstamo: {ex.Message}");
            }
        }

        public (bool Success, string Message, List<Loan> Loans) RetrieveLoansByUserId(int userId)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    var loans = r.RetrieveAll<Loan>().Where(loan => loan.user_id == userId).ToList();
                    if (loans == null || loans.Count == 0)
                    {
                        return (false, "No se encontraron préstamos para este usuario.", null);
                    }
                    return (true, "Préstamos recuperados exitosamente", loans);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar los préstamos: {ex.Message}", null);
            }
        }

        // Método para validar la elegibilidad del usuario para el préstamo
        public (bool IsEligible, string Message) ValidateLoanEligibility(Loan loan)
        {
            var userDataLogic = new UserDataLogic();
            var (success, message, userData) = userDataLogic.RetrieveByIdUserData(loan.user_id);

            if (!success || userData == null)
            {
                return (false, "No se encontraron datos del usuario para validar el préstamo.");
            }

            // Calculamos la cuota mensual usando la fórmula de amortización
            decimal monthlyInterestRate = loan.interest_rate / 100 / 12;
            int totalPayments = loan.payment_term_months;
            decimal monthlyPayment = loan.requested_amount * monthlyInterestRate *
                                     (decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments) /
                                     ((decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments) - 1);

            // Ejemplo de validación: la cuota mensual no debe superar el 30% del salario del usuario
            decimal maxAllowedPayment = userData.salary * 0.30m;
            if (monthlyPayment > maxAllowedPayment)
            {
                return (false, $"El monto solicitado genera una cuota mensual de {monthlyPayment:C} que excede el 30% de su salario.");
            }

            // Validación adicional: por ejemplo, que el usuario tenga empleo fijo.
            // Supongamos que employment_status == 1 significa que tiene empleo fijo.
            if (userData.employment_status != 1)
            {
                return (false, "El usuario no cuenta con un empleo fijo para acceder al préstamo.");
            }

            // Si se cumplen las condiciones:
            return (true, "El usuario es elegible para el préstamo.");
        }


        public (bool Success, string Message, LoanDetailsDTO Data) RetrieveLoanDetails(int loanId)
        {
            try
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    // 1. Recuperar el préstamo
                    var loan = r.Retrieve<Loan>(l => l.loan_id == loanId);
                    if (loan == null)
                    {
                        return (false, "Préstamo no encontrado", null);
                    }

                    // 2. Recuperar el usuario a través de UserLogic
                    var userLogic = new UserLogic();
                    var userResult = userLogic.RetrieveByIdUser(loan.user_id);

                    if (!userResult.Success || userResult.User == null)
                    {
                        return (false, "Usuario no encontrado para este préstamo", null);
                    }

                    var user = userResult.User;
                    // La fecha de inicio se toma de la solicitud del préstamo.
                    DateTime? startDate = loan.application_date;
                    // Por defecto, no se asigna fecha final (EndDate) hasta que se encuentre un pago.
                    DateTime? endDate = null;

                    // Consultar las amortizaciones del préstamo
                    var amortizations = r.RetrieveAll<Amortization>()
                                         .Where(a => a.loan_id == loan.loan_id)
                                         .ToList();

                    if (amortizations.Any())
                    {
                        // Ordenar las cuotas de manera descendente y obtener la última cuota
                        var lastInstallment = amortizations
                                                .OrderByDescending(a => a.installment_number)
                                                .FirstOrDefault();

                        // Solo asignar EndDate si la última cuota ya fue pagada
                        if (lastInstallment != null && lastInstallment.payment_date.HasValue)
                        {
                            endDate = lastInstallment.payment_date;
                        }
                    }

                    // Construir el DTO
                    var detailsDto = new LoanDetailsDTO
                    {
                        UserFullName = $"{user.first_name} {user.last_name}",
                        Address = user.address,
                        Email = user.email,
                        RequestedAmount = loan.requested_amount,
                        PaymentTermMonths = loan.payment_term_months,
                        StartDate = startDate,
                        EndDate = endDate
                    };

                    return (true, "Datos del préstamo recuperados exitosamente", detailsDto);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al recuperar los datos del préstamo: {ex.Message}", null);
            }
        }


    }
}
