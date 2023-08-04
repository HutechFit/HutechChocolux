using Hutech.Domain.Entities;
using Hutech.Domain.Interfaces;

namespace Hutech.Application.Services;

public class CategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public IEnumerable<Category> GetAll()
        => _unitOfWork.CategoryRepository.GetAll();
}