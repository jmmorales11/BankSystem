using DAL;
using Entities;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class LoanLogic
    {
        public (bool Success, string Message, Loan CreatedLoan) Create(Loan loan)
        {
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
    }
}
