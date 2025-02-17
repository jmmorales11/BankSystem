using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AmortizationLogic
    {
        public Amortization CreateAmortization(Amortization amortization)
        {
            Amortization res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {

                res = r.Create(amortization);

                return res;
            }

        }

        public List<Amortization> RetrieveAllAmortization()
        {
            List<Amortization> res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.RetrieveAll<Amortization>();
            }
            return res;
        }

        public Amortization RetrieveByIdAmortization(int id)
        {
            Amortization res = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Retrieve<Amortization>(p => p.amortization_id == id);
            }
            return res;
        }

        public bool Delete(int id)
        {
            bool res = false;
            var amortization = RetrieveByIdAmortization(id);
            if (amortization != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    res = r.Delete(amortization);
                }
            }
            return res;
        }
        public bool UpdateAmortization(Amortization amortization)
        {
            bool res = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                res = r.Update(amortization);  // Esto ahora está bien porque r.Update retorna un bool
            }
            return res;  // Devuelve el resultado booleano indicando si la actualización fue exitosa o no
        }

    }
}
