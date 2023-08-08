using AutoMapper;
using Hutech.Application.Services;
using Hutech.Application.ViewModels;
using Hutech.Domain.Entities;
using Hutech.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hutech.Presentation.Pages;

public class AddModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;

    [BindProperty]
    public ProductVm ProductVm { get; set; } = null!;

    [ViewData]
    public IEnumerable<SelectListItem> Categories { get; set; }

    [ViewData]
    public IEnumerable<SelectListItem> ProductStatus { get; set; } = new List<SelectListItem>
    {
        new() { Value = "0", Text = "In stock" },
        new() { Value = "1", Text = "Out of stock" }
    };

    public AddModel(
        ProductService productService,
        CategoryService categoryService,
        IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
        Categories = _mapper
            .Map<IEnumerable<Category>>(_categoryService.GetAll())
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
    }

    public void OnPost()
    {
        if (!ModelState.IsValid)
            return;

        if (!UploadImage(Request.Form.Files["ProductVm.Image"], out var fileName))
            return;

        ProductVm = ProductVm with
        {
            Image = fileName,
            Status = ProductVm.Status switch
            {
                0 => Status.InStock,
                _ => Status.OutOfStock
            }
        };
        _productService.Add(_mapper.Map<Product>(ProductVm));
        Response.Redirect("Management");
    }

    public  bool UploadImage(IFormFile? file, out string fileName)
    {
        fileName = string.Empty;
        if (file is null || file.Length == 0)
            return false;

        if (file.Length > 1024 * 1024)
        {
            ModelState
                .AddModelError("ProductVm.Image", "Image must be less than 1MB");
            return false;
        }
        
        var extension = Path.GetExtension(file.FileName);
        if (extension is not (".jpg" or ".png"))
        {
            ModelState
                .AddModelError("ProductVm.Image", "Image must be jpg or png");
            return false;
        }

        fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path
            .Combine(Directory
                .GetCurrentDirectory(),
                "wwwroot", "images", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        return true;
    }
}