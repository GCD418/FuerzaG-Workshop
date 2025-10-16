using FuerzaG.Application.Services;
using FuerzaG.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using FuerzaG.Infrastructure.Connection;
using FuerzaG.Infrastructure.Persistence.Factories;

namespace FuerzaG.Pages.Owners;

public class EditModel : PageModel
{
    private readonly OwnerService  _ownerService;
    private readonly IDataProtector _protector;

    public EditModel(OwnerService ownerService, IDataProtectionProvider provider)
    {
        _ownerService = ownerService;
        _protector = provider.CreateProtector("OwnerProtector");
    }

    
    [BindProperty] public string EncryptedId { get; set; } = string.Empty;
    [BindProperty] public int Id { get; set; }
    [BindProperty] public string Name { get; set; } = string.Empty;
    [BindProperty] public string FirstLastname { get; set; } = string.Empty;
    [BindProperty] public string? SecondLastname { get; set; }
    [BindProperty] public int PhoneNumber { get; set; }
    [BindProperty] public string Email { get; set; } = string.Empty;
    [BindProperty] public string Ci { get; set; } = string.Empty;
    [BindProperty] public string Address { get; set; } = string.Empty;

    
    public IActionResult OnGet(string id)
    {
        var decryptedId = int.Parse(_protector.Unprotect(id));
        Owner owner = _ownerService.GetById(decryptedId);
        if (owner is null) return RedirectToPage("/Owners/OwnerPage");

        EncryptedId = id;
        Id = owner.Id;
        Name = owner.Name;
        FirstLastname = owner.FirstLastname;
        SecondLastname = owner.SecondLastname;
        PhoneNumber = owner.PhoneNumber;
        Email = owner.Email;
        Ci = owner.Ci;
        Address = owner.Address;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var isSuccess = _ownerService.Update(new Owner
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

        if (!isSuccess)
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el registro.");
            return Page();
        }

        return RedirectToPage("/Owners/OwnerPage");
    }
}
