﻿using Hutech.Domain.Entities;
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
            .GetLazy(q => q.Include(x => x.Category));

    public Product GetById(int id)
        => _unitOfWork.ProductRepository.Get(x => x.Id == id);

    public IEnumerable<Product> GetPaginated(
        int pageIndex,
        int pageSize,
        string? sortColumn = null,
        string? search = null)
        => _unitOfWork.ProductRepository
            .GetList(new()
            {
                IncludeProperties = nameof(Category),
                Skip = (pageIndex - 1) * pageSize,
                Take = pageSize,
                OrderBy = x => sortColumn switch
                {
                    "Name" => x.OrderBy(p => p.Name),
                    "Price" => x.OrderBy(p => p.Price),
                    "Category" => x.OrderBy(p => p.Category!.Name),
                    "Status" => x.OrderBy(p => p.Status),
                    _ => x.OrderBy(p => p.Id)
                },
                Filter = x => search == null || x.Name!.Contains(search)
            });

    public IEnumerable<Product> ShapeData(
        IEnumerable<Product> products,
        string fields)
        => _unitOfWork.ProductRepository
            .ShapeData(products, fields);

    public int Count()
        => _unitOfWork.ProductRepository.Count(x => true);

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