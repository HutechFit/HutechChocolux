using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class MenuModel : PageModel
{
    public void OnGet()
    {
        ViewData["Data"] = null;
    }
}