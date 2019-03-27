using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes);
        T Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes);
        T GetById(int Id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Delete(int Id);
    }
}
