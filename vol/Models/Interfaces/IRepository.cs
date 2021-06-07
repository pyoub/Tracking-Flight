using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vol.Models.Entity;

namespace vol.Models.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
            Task<TEntity> Get(long id);       
            void Add(TEntity entity);   
            void Delete(TEntity entity);   
            void Update(TEntity entity);   
            Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> expression);

            IQueryable<TEntity> GetAll();

    }
}