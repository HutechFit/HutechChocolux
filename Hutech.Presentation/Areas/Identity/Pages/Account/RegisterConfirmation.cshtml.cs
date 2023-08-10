﻿using Microsoft.AspNetCore.Authorization;
using System.Text;
using Hutech.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Hutech.Presentation.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    public IEmailSender Sender { get; }

    public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSender sender)
        => (_userManager, Sender) = (userManager, sender);

    public string? Email { get; set; }

    public bool DisplayConfirmAccountLink { get; set; }

    public string? EmailConfirmationUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(string? email, string? returnUrl = null)
    {
        if (email is null)
            return RedirectToPage("/Index");

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return NotFound($"Unable to load user with email '{email}'.");

        Email = email;
        // Once you add a real email sender, you should remove this code that lets you confirm the account
        DisplayConfirmAccountLink = true;
        if (!DisplayConfirmAccountLink) return Page();

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        EmailConfirmationUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId, code, returnUrl },
            protocol: Request.Scheme);

        return Page();
    }
}