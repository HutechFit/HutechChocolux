using Hutech.Domain.Entities;
using Hutech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hutech.Application.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public IEnumerable<Product> GetAll() 
        => _unitOfWork.ProductRepository
            .GetLazy(q => q
                .Include(x => x.Category));

    public Product GetById(int id)
        => _unitOfWork.ProductRepository.Get(x => x.Id == id);

    public IEnumerable<Product> Query(string keyword)
        => _unitOfWork.ProductRepository.GetMany(x => x.Name!.Contains(keyword));

    public void Add(Product product)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.Save();
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
        }
    }

    public void Delete(int id)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.ProductRepository.Delete(x => x.Id == id);
            _unitOfWork.Save();
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
        }
    }

    public void Update(Product product)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
            _unitOfWork.Commit();
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
        }
    }
}