﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Hutech.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hutech.Presentation.Areas.Identity.Pages.Account.Manage;

public class DownloadPersonalDataModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DownloadPersonalDataModel> _logger;

    public DownloadPersonalDataModel(
        UserManager<ApplicationUser> userManager,
        ILogger<DownloadPersonalDataModel> logger)
        => (_userManager, _logger) = (userManager, logger);

    [RequiresUnreferencedCode("Personal data is retrieved from the user's instance via reflection.")]
    [RequiresDynamicCode("Personal data is retrieved from the user's instance via reflection.")]
    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

        var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
        var personalData = 
            personalDataProps
                .ToDictionary(p => p.Name, 
                    p => p.GetValue(user)?.ToString() ?? "null");

        var logins = await _userManager.GetLoginsAsync(user);
        foreach (var l in logins)
            personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);

        Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
        return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
    }
}