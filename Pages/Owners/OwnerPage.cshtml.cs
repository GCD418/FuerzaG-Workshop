using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using FuerzaG.Pages.Shared;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Owners;

public class OwnerPage : SecurePageModel
{
    public List<Owner> Owners { get; set; } = [];
    private readonly OwnerService  _ownerService;
    private readonly IDataProtector _protector;

    public OwnerPage(OwnerService ownerService, IDataProtectionProvider provider)
    {
        _ownerService = ownerService;
        _protector = provider.CreateProtector("OwnerProtector");
    }
    
    public IActionResult OnGet()
    {
        if (!ValidateSession(out var role)) return new EmptyResult();
        if (role != UserRoles.Manager) return RedirectToPage("/Owners/OwnerPage");
        Owners = _ownerService.GetAll();
        return Page();
    }
    
    public string EncryptId(int id)
    {
        return _protector.Protect(id.ToString());
    }
    
    
    public IActionResult OnPostDelete(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        _ownerService.DeleteById(decryptedId);
        return RedirectToPage();
    }

    
}