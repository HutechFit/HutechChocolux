using Hutech.Domain.Specifications;
using System.Linq.Expressions;

namespace Hutech.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    public IEnumerable<T> GetAll();
    public void Insert(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public int Count(Expression<Func<T, bool>> predicate);
    public IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    public IEnumerable<T> GetList(Criteria<T> criteria);
    public bool Any(Expression<Func<T, bool>> where);
}