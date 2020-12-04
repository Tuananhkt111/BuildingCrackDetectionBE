using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(object id);

        Task<T> GetSingle(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int limit = 0, int offset = 0);

        void Create(T obj);

        void Update(T obj);
    }
}