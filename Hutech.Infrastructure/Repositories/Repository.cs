using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Hutech.Domain.Interfaces;
using Hutech.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Hutech.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
        => (_context, _dbSet) = (context, context.Set<T>());

    public virtual IEnumerable<T> GetAll()
        => _dbSet.AsNoTracking();

    public virtual IEnumerable<T> GetLazy(Func<IQueryable<T>, IQueryable<T>> func)
        => func(_dbSet.AsNoTracking());

    public virtual void Insert(T entity)
        => _dbSet.Add(entity);

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

    public virtual void Delete(Expression<Func<T, bool>> where)
    {
        var objects = _dbSet.Where(where).AsEnumerable();
        foreach (var obj in objects)
            _dbSet.Remove(obj);
    }

    public virtual int Count(Expression<Func<T, bool>> predicate)
        => _dbSet.Count(predicate);

    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        => _dbSet.Where(where);

    public virtual IEnumerable<T> GetList(Criteria<T> criteria)
    {
        IQueryable<T> query = _dbSet;
        if (criteria.Filter is { })
            query = query.Where(criteria.Filter);

        query = criteria
            .IncludeProperties?
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty)
                => current.Include(includeProperty)) ?? query;

        if (criteria.OrderBy is { })
            query = criteria.OrderBy(query);

        if (criteria.Skip != 0)
            _ = query.Skip(criteria.Skip);

        if (criteria.Take != 0)
            _ = query.Take(criteria.Take);

        return query;
    }

    public IEnumerable<T> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        if (string.IsNullOrWhiteSpace(fieldsString))
            return entities;

        var fields = fieldsString.Split(',');

        var shapedEntities = new List<ExpandoObject>();

        foreach (var entity in entities)
        {
            var dataShapedObject = new ExpandoObject();

            foreach (var field in fields)
            {
                var propertyInfo = typeof(T)
                    .GetProperty(field.Trim(),
                        BindingFlags.IgnoreCase
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                if (propertyInfo is null)
                    continue;

                var propertyValue = propertyInfo.GetValue(entity);
                (dataShapedObject as IDictionary<string, object>)
                    .Add(propertyInfo.Name, propertyValue ?? string.Empty);
            }

            shapedEntities.Add(dataShapedObject);
        }

        return (IEnumerable<T>)shapedEntities;
    }

    public virtual bool Any(Expression<Func<T, bool>> where)
        => _dbSet.Any(where);

    public virtual T Get(Expression<Func<T, bool>> where)
        => _dbSet.FirstOrDefault(where)
           ?? throw new NullReferenceException();
}