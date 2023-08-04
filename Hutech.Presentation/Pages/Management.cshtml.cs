using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class ManagementModel : PageModel
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    public ManagementModel(ProductService productService, IMapper mapper)
        => (_productService, _mapper) = (productService, mapper);

    public void OnGet()
        => ViewData["Products"] = _mapper
                .Map<IEnumerable<ProductResponse>>(_productService.GetAll());

    public void OnGetSearch(string search)
        => ViewData["Products"] = null;
}