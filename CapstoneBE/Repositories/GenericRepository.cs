using CapstoneBE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal CapstoneDbContext _capstoneDbContext;
        internal DbSet<T> _dbSet;

        public GenericRepository(CapstoneDbContext capstoneDbContext)
        {
            _capstoneDbContext = capstoneDbContext;
            _dbSet = capstoneDbContext.Set<T>();
        }

        public virtual void Create(T obj)
        {
            _dbSet.Add(obj);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int limit = 0, int offset = 0)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            if (offset > 0)
                query = query.Skip(offset);
            if (limit > 0)
                query = query.Take(limit);
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
                return orderBy(query);
            return query;
        }

        public virtual async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual Task<T> GetSingle(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.SingleOrDefaultAsync();
        }

        public virtual void Update(T obj)
        {
            _dbSet.Attach(obj);
            _dbSet.Update(obj);
        }
    }
}