using System.Linq.Expressions;

namespace Hutech.Domain.Specifications;

public class Criteria<T> where T : class
{
    public Expression<Func<T, bool>>? Filter = null;
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy = null;
    public string? IncludeProperties = "";
    public int Skip = 0;
    public int Take = 0;
}