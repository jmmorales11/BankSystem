using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class EFRepository : IRepository
    {
        DbContext _dbContext;

        public EFRepository(DbContext dbContext)
        {
            string connectionString = ConnectionStringHelper.GetConnectionString("BankEntities");

            // Crea el DbContext usando la cadena de conexión actualizada
            _dbContext = new DbContext(connectionString);
            _dbContext.Configuration.LazyLoadingEnabled = false;
        }

        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {
            TEntity result = default;
            try
            {
                _dbContext.Set<TEntity>().Add(toCreate);
                _dbContext.SaveChanges();
                result = toCreate;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
        {
            bool result = false;
            try
            {
                _dbContext.Entry<TEntity>(toDelete).State = EntityState.Deleted;
                result = _dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> result = null;
            try
            {
                result = _dbContext.Set<TEntity>().Where(criteria).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity result = null;
            try
            {
                result = _dbContext.Set<TEntity>().FirstOrDefault(criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            bool result = false;
            try
            {
                _dbContext.Entry<TEntity>(toUpdate).State = EntityState.Modified;
                result = _dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        // Nuevo método para recuperar todos los registros
        public List<TEntity> RetrieveAll<TEntity>() where TEntity : class
        {
            List<TEntity> result = null;
            try
            {
                // Asegúrate de que el DbContext esté accesible y no se cierre prematuramente
                result = _dbContext.Set<TEntity>().ToList(); // Recupera todos los registros
            }
            catch (Exception ex)
            {
                // Log o manejo de la excepción
                throw new Exception("Error al recuperar todos los registros", ex);
            }
            return result;
        }

    }
}
