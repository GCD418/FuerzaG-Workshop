using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Services;


[Authorize(Roles = UserRoles.Manager)]
public class ServicePage : PageModel
{
    public List<Service> Services { get; set; } = new();
    private readonly ServiceService _serviceService;
    private readonly IDataProtector _protector;

    public ServicePage(ServiceService serviceService, IDataProtectionProvider provider)
    {
        _serviceService = serviceService;
        _protector = provider.CreateProtector("ServiceProtector");
    }

    public void OnGet()
    {
        Services = _serviceService.GetAll();
    }
    
    public string EncryptId(int id)
    {
        return _protector.Protect(id.ToString());
    }
    public IActionResult OnPostDelete(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        _serviceService.DeleteById(decryptedId);
        return RedirectToPage();
    }

}