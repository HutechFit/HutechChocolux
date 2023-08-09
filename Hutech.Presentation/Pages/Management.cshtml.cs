using Hutech.Application.Services;
using Hutech.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class ManagementModel : PageModel
{
    private readonly ProductService _productService;

    public ManagementModel(ProductService productService)
        => _productService = productService;

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string SortBy { get; set; } = "Id";

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; } = string.Empty;

    public int Count { get; set; }
    public int PageSize { get; set; } = 10;

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

    [ViewData]
    public IEnumerable<Product> Products { get; set; } = new List<Product>();

    public void OnGet()
    {
        Products = _productService.GetPaginated(CurrentPage, PageSize, SortBy, Search);
        Count = _productService.Count();
    }

    public void OnGetDelete(int id)
    {
        _productService.Delete(id);
        Response.Redirect("Management");
    }
}