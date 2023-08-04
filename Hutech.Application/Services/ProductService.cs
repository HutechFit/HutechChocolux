using Hutech.Application.Models;
using Hutech.Domain.Entities;
using Hutech.Domain.Interfaces;

namespace Hutech.Application.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public IEnumerable<Product> GetAll() 
        => _unitOfWork.ProductRepository.GetAll();

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
}