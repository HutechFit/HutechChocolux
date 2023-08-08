using Hutech.Domain.Entities;

namespace Hutech.Domain.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Product> ProductRepository { get; }
    public IRepository<Category> CategoryRepository { get; }
    public void BeginTransaction();
    public void Commit();
    public void Rollback();
    public void Save();
}