using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Pages.Owners;

public class EditModel : PageModel
{
    private readonly OwnerRepositoryCreator _creator;

    public EditModel(IDbConnectionFactory connectionFactory)
        => _creator = new OwnerRepositoryCreator(connectionFactory);

    
    [BindProperty] public int Id { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string FirstLastname { get; set; } = string.Empty;
    [BindProperty] public string? SecondLastname { get; set; }
    [BindProperty] public int PhoneNumber { get; set; }
    [BindProperty] public string Email { get; set; } = string.Empty;
    [BindProperty] public string Ci { get; set; } = string.Empty;
    [BindProperty] public string Address { get; set; } = string.Empty;

    
    public IActionResult OnGet(int id)
    {
        var repo = _creator.GetRepository<Owner>();
        var o = repo.GetById(id);
        if (o is null) return RedirectToPage("/Owners/OwnerPage");

        
        Id = o.Id;
        Name = o.Name;
        FirstLastname = o.FirstLastname;
        SecondLastname = o.SecondLastname;
        PhoneNumber = o.PhoneNumber;
        Email = o.Email;
        Ci = o.Ci;
        Address = o.Address;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var repo = _creator.GetRepository<Owner>();
        var ok = repo.Update(new Owner
        {
            Id = Id,
            Name = Name,
            FirstLastname = FirstLastname,
            SecondLastname = SecondLastname,
            PhoneNumber = PhoneNumber,
            Email = Email,
            Ci = Ci,
            Address = Address
        });

        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el registro.");
            return Page();
        }

        return RedirectToPage("/Owners/OwnerPage");
    }
}
