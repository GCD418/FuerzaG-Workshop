using FuerzaG.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FuerzaG.Pages.Servicess;

public class DeleteModel : PageModel
{
    private readonly ServiceService _serviceService;

    public DeleteModel(ServiceService serviceService)
    {
        _serviceService = serviceService;
    }


    public IActionResult OnPost(int id)
    {
        _serviceService.DeleteById(id);
        return RedirectToPage("/Servicess/ServicePage");
    }
}