using Hutech.Domain.Entities;
using Hutech.Domain.Interfaces;

namespace Hutech.Infrastructure.Repositories;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IRepository<Category>? _categoryRepository;

    private IRepository<Product>? _productRepository;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public IRepository<Product> ProductRepository
        => _productRepository ??= new Repository<Product>(_context);

    public IRepository<Category> CategoryRepository
        => _categoryRepository ??= new Repository<Category>(_context);

    public void BeginTransaction() => _context.Database.BeginTransaction();

    public void Commit() => _context.Database.CommitTransaction();

    public void Rollback() => _context.Database.RollbackTransaction();

    public void Save() => _context.SaveChanges();
}