using AutoMapper;
using Hutech.Application.Services;
using Hutech.Application.ViewModels;
using Hutech.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hutech.Presentation.Pages;

public class EditModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;

    [ViewData]
    public IEnumerable<SelectListItem> Categories { get; set; }

    [ViewData] 
    public ProductResponse Product { get; set; } = null!;

    [BindProperty]
    public Product ProductRequest { get; set; } = null!;

    public EditModel(
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

    public void OnGet(int id)
        => Product = _mapper.Map<ProductResponse>(_productService.GetById(id));

    public void OnGetDeleteImage(int id)
    {
        var product = _productService.GetById(id);
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", product.Image ?? string.Empty);
        System.IO.File.Delete(path);
        product.Image = string.Empty;
        _productService.Update(product);
        RedirectToPage("Edit", new { id });
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        _productService.Update(ProductRequest);
        return RedirectToPage("Index");
    }
}