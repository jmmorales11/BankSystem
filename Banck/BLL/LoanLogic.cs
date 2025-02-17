using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoanLogic
    {
        public Loan Create(Loan users)
        {
            Loan res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                users.status = 1;
                users.application_date = DateTime.Now;

                res = r.Create(users);

                return res;
            }

        }

        public List<Loan> RetrieveAllLoan()
        {
            List<Loan> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<Loan>();
            }
            return res;
        }

        public Loan RetrieveByIdLoan(int id)
        {
            Loan res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Loan>(p => p.loan_id == id);
            }
            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;
            var product = RetrieveByIdLoan(id);
            if (product != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(product);
                }
            }
            return res;
        }


    }
}