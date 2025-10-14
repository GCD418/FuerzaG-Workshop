using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Servicess;

public class ServicePage : PageModel
{
    public List<Service> Services { get; set; } = new();
    private readonly ServiceService _serviceService;

    public ServicePage(IDbConnectionFactory connectionFactory)
    {
        _serviceService = serviceService;
    }

    public void OnGet()
    {
        Servicess = _serviceService.GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
        _serviceService.DeleteById(id);
        return RedirectToPage();
    }
}
