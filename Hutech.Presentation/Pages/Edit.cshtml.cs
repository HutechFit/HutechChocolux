using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
using Hutech.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class EditModel : PageModel
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IMapper _mapper;


    [ViewData]
    public IEnumerable<CategoryResponse> Categories { get; set; }

    [ViewData] 
    public ProductResponse Product { get; set; } 

    [BindProperty]
    public ProductRequest ProductRequest { get; set; } =  null!;

    public EditModel(
        ProductService productService,
        CategoryService categoryService,
        IMapper mapper, ProductResponse product)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
        Product = product;
        Categories = _mapper
            .Map<IEnumerable<CategoryResponse>>(_categoryService.GetAll());
    }

    public void OnGet([FromQuery] int id)
        => Product = _mapper.Map<ProductResponse>(_productService.GetById(id));

    public void OnPost()
    {
        _productService.Update(_mapper.Map<Product>(ProductRequest));
        Response.Redirect("Management");
    }
    
}