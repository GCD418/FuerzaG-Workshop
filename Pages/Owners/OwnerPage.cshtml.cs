using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Owners;

public class OwnerPage : PageModel
{
    public List<Owner> Owners { get; set; } = [];
    private readonly OwnerService  _ownerService;
    private readonly IDataProtector _protector;

    public OwnerPage(OwnerService ownerService, IDataProtectionProvider provider)
    {
        _ownerService = ownerService;
        _protector = provider.CreateProtector("OwnerProtector");
    }
    
    public void OnGet()
    {
        Owners = _ownerService.GetAll();
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