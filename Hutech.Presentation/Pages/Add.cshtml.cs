using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
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
    public ProductRequest ProductRequest { get; set; } = null!;

    [ViewData]
    public IEnumerable<SelectListItem> Categories { get; set; }

    [ViewData]
    public IEnumerable<SelectListItem> ProductStatus { get; set; } = new List<SelectListItem>
    {
        new() { Value = "0", Text = "Out of stock" },
        new() { Value = "1", Text = "In stock" }
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
            .Map<IEnumerable<CategoryResponse>>(_categoryService.GetAll())
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

        if (!UploadImage(Request.Form.Files["ProductRequest.Image"], out var fileName))
            return;

        ProductRequest = ProductRequest with
        {
            Image = fileName,
            Status = ProductRequest.Status switch
            {
                0 => Status.InStock,
                _ => Status.OutOfStock
            }
        };
        _productService.Add(_mapper.Map<Product>(ProductRequest));
        Response.Redirect("Management");
    }

    public bool UploadImage(IFormFile? file, out string fileName)
    {
        fileName = string.Empty;
        if (file is null || file.Length == 0)
        {
            ModelState.AddModelError("ProductRequest.Image", "Image is required");
            return false;
        }

        if (file.Length > 1024 * 1024)
        {
            ModelState.AddModelError("ProductRequest.Image", "Image must be less than 1MB");
            return false;
        }
        
        var extension = Path.GetExtension(file.FileName);
        if (extension is not (".jpg" or ".png"))
        {
            ModelState.AddModelError("ProductRequest.Image", "Image must be jpg or png");
            return false;
        }

        fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        return true;
    }
}