using AutoMapper;
using Hutech.Application.Models;
using Hutech.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class ManagementModel : PageModel
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    [ViewData] 
    public IEnumerable<ProductResponse> Products { get; set; }

    public ManagementModel(ProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
        Products = _mapper
            .Map<IEnumerable<ProductResponse>>(_productService.GetAll());
    }

    public void OnGetSearch(string search)
        => throw new NotImplementedException();

    public void OnGetDelete(int id)
    {
        _productService.Delete(id);
        Response.Redirect("Management");
    }
}