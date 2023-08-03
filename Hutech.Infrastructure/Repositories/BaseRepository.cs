using System.Linq.Expressions;
using Hutech.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Hutech.Infrastructure.Repositories;

public abstract class BaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(ApplicationDbContext context) 
        => (_context, _dbSet) = (context, context.Set<T>());

    public virtual IEnumerable<T> GetAll() => _dbSet.AsNoTracking();

    public virtual void Insert(T entity) => _dbSet.Add(entity);

    public virtual void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
        _dbSet.Remove(entity);
    }

    public virtual int Count(Expression<Func<T, bool>> predicate) => _dbSet.Count(predicate);

    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => _dbSet.Where(where);

    public virtual IEnumerable<T> GetList(Criteria<T> criteria)
    {
        IQueryable<T> query = _dbSet;
        if (criteria.Filter is not null)
            query = query.Where(criteria.Filter);

        query = criteria
            .IncludeProperties?
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) 
                => current.Include(includeProperty)) ?? query;

        if (criteria.OrderBy is not null)
            query = criteria.OrderBy(query);

        if (criteria.Skip != 0)
            _ = query.Skip(criteria.Skip);

        if (criteria.Take != 0)
            _ = query.Take(criteria.Take);

        return query;
    }

    public virtual bool Any(Expression<Func<T, bool>> where) => _dbSet.Any(where);
}