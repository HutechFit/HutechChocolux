﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
        => _logger = logger;

    public void OnGet()
    {

    }

    public void OnGetUser()
    {

    }
}