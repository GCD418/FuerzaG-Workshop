using FuerzaG.Application.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Services;

public class DeleteModel : PageModel
{
    private readonly ServiceService _serviceService;
    private readonly IDataProtector _protector;

    public DeleteModel(ServiceService serviceService, IDataProtectionProvider provider)
    {
        _serviceService = serviceService;
        _protector = provider.CreateProtector("ServiceProtector");
    }


    public IActionResult OnPost(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        _serviceService.DeleteById(decryptedId);
        return RedirectToPage("/Services/ServicePage");
    }
}