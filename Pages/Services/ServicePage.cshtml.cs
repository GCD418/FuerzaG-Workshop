using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Services;

public class ServicePage : PageModel
{
    public List<Service> Services { get; set; } = new();
    private readonly ServiceService _serviceService;

    public ServicePage(ServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public void OnGet()
    {
        Services = _serviceService.GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
        _serviceService.DeleteById(id);
        return RedirectToPage();
    }
}
