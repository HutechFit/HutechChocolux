using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
using Hutech.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class AddModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;

    [BindProperty]
    public ProductRequest ProductVm { get; set; } = null!;

    [ViewData]
    public IEnumerable<CategoryResponse> Categories { get; set; }

    public AddModel(
        ProductService productService,
        CategoryService categoryService,
        IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
        Categories = _mapper
            .Map<IEnumerable<CategoryResponse>>(_categoryService.GetAll());
    }


    public void OnPost()
    {
        if (!ModelState.IsValid)
        {
            Categories = _mapper
                .Map<IEnumerable<CategoryResponse>>(_categoryService.GetAll());

            return;
        }

        if (!UploadImage(Request.Form.Files["Image"], out var fileName))
        {
            Categories = _mapper
                .Map<IEnumerable<CategoryResponse>>(_categoryService.GetAll());
            ModelState.AddModelError("ProductVm.Image", "Invalid image");
            return;
        }

        ProductVm = ProductVm with { Image = fileName };
        _productService.Add(_mapper.Map<Product>(ProductVm));
        Response.Redirect("Management");
    }

    private bool UploadImage(IFormFile? file, out string fileName)
    {
        fileName = string.Empty;
        if (file is null || file.Length == 0)
            return false;

        var extension = Path.GetExtension(file.FileName);
        if (extension is not ".jpg" and not ".png")
            return false;

        fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        return true;
    }
}