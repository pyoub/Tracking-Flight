using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vol.Models.Context;
using vol.Models.Entity;
using vol.Models.Factory;
using vol.Models.Interfaces;

namespace vol.Models.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly DbFactory _dbFactory;   
        private DbSet<TEntity> _dbSet;   
    
        protected DbSet<TEntity> DbSet {get; init;}
        public Repository(DbFactory dbFactory){
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            _dbSet = dbFactory.DbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
             _dbSet.Remove(entity);
        }

        public async Task<TEntity> Get(long id)
        {
            return await _dbSet.SingleOrDefaultAsync(_ => _.Id == id).ConfigureAwait(false);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync().ConfigureAwait(false);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}