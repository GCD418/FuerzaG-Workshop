using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Owners;

public class OwnerPage : PageModel
{
    public List<Owner> Owners { get; set; } = [];
    private readonly OwnerService  _ownerService;

    public OwnerPage(OwnerService ownerService)
    {
        _ownerService = ownerService;
    }
    public void OnGet()
    {
        Owners = _ownerService.GetAll();
    }
    
    
public IActionResult OnPostDelete(int id)
{
    _ownerService.DeleteById(id);
    return RedirectToPage();
}

    
}