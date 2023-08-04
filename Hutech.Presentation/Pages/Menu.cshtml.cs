using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class MenuModel : PageModel
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public MenuModel(ProductService productService, IMapper mapper)
        => (_productService, _mapper) = (productService, mapper);

    public void OnGet()
    {
        var products = _mapper.Map<IEnumerable<ProductResponse>>(_productService.GetAll());
        ViewData["Products"] = products;
    }
}