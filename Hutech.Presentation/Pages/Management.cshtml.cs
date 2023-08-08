using Hutech.Application.Services;
using Hutech.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class ManagementModel : PageModel
{
    private readonly ProductService _productService;

    [ViewData]
    public IEnumerable<Product> Products { get; set; } = new List<Product>();

    public ManagementModel(ProductService productService)
        => _productService = productService;

    public void OnGet()
        => Products = _productService.GetAll();

    public void OnGetSearch(string search)
    {
        Products = _productService.Query(search);
        ViewData["search"] = search;
    }

    public void OnGetDelete(int id)
    {
        _productService.Delete(id);
        Response.Redirect("Management");
    }
}