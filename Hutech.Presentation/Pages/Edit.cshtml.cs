using AutoMapper;
using Hutech.Application.Services;
using Hutech.Application.ViewModels;
using Hutech.Domain.Entities;
using Hutech.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class EditModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;

    [ViewData]
    public IEnumerable<Category> Categories { get; set; }

    [ViewData]
    public Product Product { get; set; } = default!;

    [BindProperty]
    public ProductVm ProductVm { get; set; } = null!;

    public EditModel(
        ProductService productService,
        CategoryService categoryService,
        IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
        Categories = _categoryService.GetAll();
    }

    public void OnGet([FromQuery] int id)
        => Product = _productService.GetById(id);

    public IActionResult OnGetDeleteImage([FromQuery] int id)
    {
        var product = _productService.GetById(id);
        var path = Path
            .Combine(Directory
                .GetCurrentDirectory(), "wwwroot", "images",
                product.Image ?? string.Empty);
        System.IO.File.Delete(path);
        product.Image = string.Empty;
        _productService.Update(product);
        return RedirectToPage("Edit", new { id });
    }

    public IActionResult OnPost()
    {
        var product = _productService.GetById(ProductVm.Id);
        if (!ModelState.IsValid)
            return RedirectToPage("Edit", new { ProductVm.Id });

        if (Request.Form.Files["ProductVm.Image"] is { })
        {
            if (!UploadImage(Request.Form.Files["ProductVm.Image"],
                    out var fileName))
                return RedirectToPage("Edit",
                    new { ProductVm.Id });

            if (product.Image is { })
            {
                var path = Path
                    .Combine(Directory
                            .GetCurrentDirectory(), "wwwroot", "images", 
                        product.Image);
                System.IO.File.Delete(path);
            }

            ProductVm = ProductVm with { Image = fileName };
        }
        else
            ProductVm = ProductVm with { Image = product.Image };

        _productService.Update(_mapper.Map<Product>(ProductVm));
        return RedirectToPage("Management");
    }

    public bool UploadImage(IFormFile? file, out string fileName)
    {
        fileName = string.Empty;
        if (file is null || file.Length == 0)
            return false;

        if (file.Length > 1024 * 1024)
        {
            ModelState.AddModelError("ProductVm.Image", "Image must be less than 1MB");
            return false;
        }

        var extension = Path.GetExtension(file.FileName);
        if (extension is not (".jpg" or ".png"))
        {
            ModelState.AddModelError("ProductVm.Image", "Image must be jpg or png");
            return false;
        }

        fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path
            .Combine(Directory
                .GetCurrentDirectory(), "wwwroot", "images", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        return true;
    }
}