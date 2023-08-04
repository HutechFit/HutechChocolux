using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace Hutech.Presentation.Pages;

public class MenuModel : PageModel
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public MenuModel(ProductService productService, IMapper mapper)
        => (_productService, _mapper) = (productService, mapper);

    public void OnGet()
    {
        var products = _mapper.Map<IEnumerable<ProductVm>>(_productService.GetAll());
        ViewData["Products"] = products;
    }
}